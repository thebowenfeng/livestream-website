# livestream-website

A livestreaming service built using Flask, SocketIO, Celery and OpenCV (.NET Core variant). Supports multiple, simultaneous streaming clients and asynchronous recording ("clipping").

## Installation

Install requirements.txt

Run celery locally (`python -m celery -A celery_task worker --loglevel=INFO`)

Run the flask server

