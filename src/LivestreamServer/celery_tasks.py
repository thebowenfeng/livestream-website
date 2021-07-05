from celery import Celery
import cv2
import os
import shutil
import time

app = Celery('celery_tasks', broker='amqps://ytbnwbml:IDVBKZNWRqP1-S9op6bFqEK67B8-4n-Z@hornet.rmq.cloudamqp.com/ytbnwbml',
             backend='db+sqlite:///backend.sqlite3')


@app.task(bind=True)
def make_video(self, dir_name: str):
    utc = str(time.time()).replace(".", "")

    self.update_state(
        state='WRITING',
        meta={
            'current': "Video processing...",
            'total': "Video processing..."
        }
    )

    size = (0, 0)
    img_list = []

    for image in os.listdir(dir_name):
        img = cv2.imread(os.path.join(dir_name, image))
        height, width, layers = img.shape
        size = (width, height)
        img_list.append(img)

    out = cv2.VideoWriter(f"{dir_name}_{utc}.avi", cv2.VideoWriter_fourcc(*'DIVX'), 30, size)

    for i in range(len(img_list)):
        out.write(img_list[i])
        self.update_state(
            state='WRITING',
            meta={
                'current': str(i+1),
                'total': str(len(img_list)+1)
            }
        )

    out.release()
    shutil.rmtree(dir_name)

    return f"{dir_name}_{utc}.avi"



