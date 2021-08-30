const WebSocket = require('ws');

const port = process.env.PORT || 8080;

const wss = new WebSocket.Server({port: port}, ()=>{
  console.log('server started');
});

wss.on('connection', (ws)=>{
  ws.on('message', (data)=>{
    console.log('data received: ' + data);
    ws.send(data.toString());
  });
});

wss.on('listening', ()=>{
  console.log('server is listening on port 8080');
});
