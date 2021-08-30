const WebSocket = require('ws');

const port = process.env.PORT || 8080;

//let connections = [];

//let myDictionary = {};

//myDictionary["02457598679"] = ws;

const wss = new WebSocket.Server({port: port}, ()=>{
  console.log('server started');
});

wss.on('connection', (ws)=>{
  
  console.log("Created Connection");
  connections.push(ws);

  ws.on('message', (data)=>{
    console.log('data received: ' + data);
    ws.send(data.toString());

    //myDictionary[data.targetWsId].send(data);

  });

  ws.on('close', () => {
    connections = connections.filter(c => c !== ws);
    console.log("Closed Connection");
  });

});

wss.on('listening', ()=>{
  console.log('server is listening on port 8080');
});
