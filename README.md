# livestream-website

A livestreaming service built using Flask, SocketIO, Celery and OpenCV (.NET Core variant). Supports multiple, simultaneous streaming clients and asynchronous recording ("clipping").

A deployed version of the website can be found [here](https://livestreaming-server.herokuapp.com/). However, due the file writing restrictions imposed via the hosting service, the recording feature is not available. The quality and speed of the streams will also be severely slow due to bandwidth limitations and the resource-consuming nature of streaming.

## Installation

### Server

Install requirements.txt

Run celery locally (`python -m celery -A celery_task worker --loglevel=INFO`)

Run the flask server

### Client

Install .NET Core 3.0 framework

Run executable in Debug folder. Ensure DLL files are placed in the same folder as the executable.

## Live demo

A live demo of the website can be found here:



