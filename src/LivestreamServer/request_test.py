import requests

r = requests.post("http://localhost:3000/api/status", data={'user_id': 'test2', 'sid': '12345678'})

print(r.text)