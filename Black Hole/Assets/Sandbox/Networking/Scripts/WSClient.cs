using WebSocketSharp;
using UnityEngine;
using System;
namespace Sandbox.Networking
{

    [Serializable]
    class MyData
    {
        public string name;
        public int age;
    }

    public class WSClient : MonoBehaviour
    {
        WebSocket ws;

        void Start()
        {
            ws = new WebSocket("ws://localhost:8080");
            ws.OnMessage += (sender, e) =>
            {
                MyData data = JsonUtility.FromJson<MyData>(e.Data);
                Debug.Log($"Message Received from {((WebSocket)sender).Url}, Data: {data.name}, {data.age}");
            };

            ws.Connect();
        }

        void Update()
        {
            if (ws == null)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MyData data = new MyData { name = "aaa", age = UnityEngine.Random.Range(0, 100) };
                ws.Send(JsonUtility.ToJson(data));
            }
        }
    }
}
