const MessageType = Object.freeze({'MESSAGE':1, 'JOIN':2, 'LEAVE':3, 'JOINCALLBACK':4})

const WebSocket = require('ws');

const port = process.env.PORT || 8080;

let connections = [];

let users = {};

const wss = new WebSocket.Server({port: port}, ()=>{
  console.log('server started');
});

wss.on('connection', (ws)=>{
  
  console.log("Created Connection");
  connections.push(ws);

  ws.send(JSON.stringify({
    messageType: MessageType.JOINCALLBACK,
    message: makeid(16),
  }));

  ws.on('message', (data)=>{
    console.log('data received: ' + data);

    for (let i = 0; i < connections.length; i++){
      connections[i].send(data.toString());
    }

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

function makeid(length) {
  var result           = '';
  var characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  var charactersLength = characters.length;
  for ( var i = 0; i < length; i++ ) {
    result += characters.charAt(Math.floor(Math.random() * 
charactersLength));
 }
 return result;
}
