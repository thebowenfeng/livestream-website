import base64
import cv2
from flask import Flask, render_template, request, url_for, send_file, send_from_directory
from flask_socketio import SocketIO, join_room, leave_room
from flask_sqlalchemy import SQLAlchemy
import numpy as np
from celery_tasks import make_video
import os
import time

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///userinfo.db'

sio = SocketIO(app)
db = SQLAlchemy(app)


class Client(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(100), unique=True, nullable=False)
    sid = db.Column(db.String(500), unique=True, nullable=False)


class Task(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(100), unique=True, nullable=False)
    task_id = db.Column(db.String(500), unique=True, nullable=False)


@app.route('/', methods=['GET'])
def index():
    return render_template("index.html")


@app.route('/api/status', methods=['POST', 'GET'])
def api_status():
    if request.method == "POST" and 'user_id' in request.form and 'sid' in request.form:
        user_id = request.form['user_id']
        sid = request.form['sid']

        if Client.query.filter_by(name=user_id).first() is not None:
            return {"status": "failure", "message": "User already streaming"}

        client = Client(name=user_id, sid=str(sid))
        db.session.add(client)
        db.session.commit()
        return {"status": "success"}
    elif request.method == "GET":
        response = {}
        all_clients = Client.query.order_by(Client.name).all()

        for client in all_clients:
            response[client.name] = client.sid

        return response
    else:
        return "Invalid request, check your request headers/args"


@app.route('/api/render_video', methods=["POST"])
def video_status():
    if request.method == "POST" and 'user_id' in request.json:
        username = request.json['user_id']
        task_stat = Task.query.filter_by(name=username).first()

        if task_stat is None:
            return {"status": "novideo"}

        task = make_video.AsyncResult(task_stat.task_id)
        if task.state == "WRITING":
            return {
                "status": "writing",
                "current": task.info['current'],
                "total": task.info['total']
                    }
        elif task.state == "PENDING":
            return {
                "status": "writing",
                "current": "Rendering task dispatched",
                "total": "Rendering task dispatched"
                    }
        elif task.state == "SUCCESS":
            filename = task.get()
            print(filename)
            db.session.delete(task_stat)
            db.session.commit()

            return {"status": "complete", "filename": filename}
        else:
            return {"status": "error"}
    else:
        return "Invalid request, check your request headers/args"


@app.route('/downloads/<string:filename>')
def download(filename):
    return send_file(filename, as_attachment=True)


@app.route('/streaming/<string:username>')
def stream(username):
    return render_template('stream.html', username=username)


@sio.on('connect', namespace="/web")
def connect():
    print("Web client connected " + request.sid)


@sio.on('join', namespace='/web')
def join_web(room):
    join_room(room, namespace='/web')
    print(request.sid + " has entered room " + room)


@sio.on("disconnect", namespace='/web')
def disconnect():
    print("Web client disconnected " + request.sid)


@sio.on("leave", namespace='/web')
def leave_web(room):
    leave_room(room, namespace='/web')
    print(request.sid + " has left room " + room)


@sio.on('connect', namespace='/stream')
def stream_connect():
    sio.emit('get_sid', str(request.sid))
    print("Streaming client connected " + request.sid)


@sio.on("disconnect", namespace='/stream')
def stream_disconnect():
    client = Client.query.filter_by(sid=str(request.sid)).first()
    username = client.name

    if os.path.isdir(username):
        task = make_video.delay(username)
        vid_task = Task(name=username, task_id=task.id)
        db.session.add(vid_task)

    db.session.delete(client)
    db.session.commit()

    sio.emit("display", "data:image/jpeg;base64,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", namespace='/web')
    print("Streaming client disconnected " + request.sid)


@sio.on('img', namespace='/stream')
def handle_img(incoming):
    username = incoming.split("|")[0]
    is_recording = incoming.split("|")[1]
    img_data = incoming.split("|")[2]

    data = "data:image/jpeg;base64,{}".format(img_data)

    sio.emit("display", data, namespace='/web', to=username)

    if is_recording == "True":
        if not os.path.isdir(username):
            os.mkdir(username)

        with open(f"{username}/{time.time()}.jpg", "wb") as file:
            file.write(base64.b64decode(img_data))

    #cv2.imshow("test", img)
    #cv2.waitKey(1)


if __name__ == "__main__":
    sio.run(app=app, host='localhost', port=3000)

