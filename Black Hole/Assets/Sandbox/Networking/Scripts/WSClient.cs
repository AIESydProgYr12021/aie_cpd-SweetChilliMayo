using WebSocketSharp;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Sandbox.Networking
{
    public enum MessageType
    {
        MESSAGE = 1,
        JOIN = 2,
        LEAVE = 3,
        JOINCALLBACK = 4,
        SETUSERNAME = 5,
    }

    [Serializable]
    class MessageData
    {
        public MessageType messageType = MessageType.MESSAGE;
        public string toUser;
        public string username;
        public string message;
        public DateTime? time;
    }

    public class WSClient : MonoBehaviour
    {
        WebSocket ws;

        public string username = "John";
        public string id = "";

        void Start()
        {
            // "ws://localhost:8080"
            // "ws://blackhole-test-server.herokuapp.com"
            JoinServer("ws://localhost:8080");
        }

        void Update()
        {
            if (ws == null)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessage(MessageType.MESSAGE, id, "Test");
            }
        }

        public void JoinServer(string url)
        {
            ws = new WebSocket(url);

            ws.OnMessage += ProcessMessage;

            ws.Connect();
        }

        public void SendMessage(MessageType messageType, string sendToUser, string message)
        {
            MessageData data = new MessageData {
                messageType = messageType,
                toUser = sendToUser, 
                username = username, 
                message = message, 
                time = DateTime.Now 
            };

            ws.Send(JsonUtility.ToJson(data));
        }

        public void SendMessage(MessageType messageType, string username)
        {
            MessageData data = new MessageData
            {
                messageType = messageType,
                username = username,
            };

            ws.Send(JsonUtility.ToJson(data));
        }

        void ProcessMessage(object sender, MessageEventArgs e)
        {
            MessageData data = JsonUtility.FromJson<MessageData>(e.Data);

            if (data.messageType == MessageType.MESSAGE)
            {
                Debug.Log($"Message: {data.username}, {data.message}");
            }
            else if (data.messageType == MessageType.JOINCALLBACK)
            {
                id = data.message;
                SendMessage(MessageType.SETUSERNAME, username);
            }
        }

        private void OnDestroy()
        {
            ws.Close();
        }
    }
}
