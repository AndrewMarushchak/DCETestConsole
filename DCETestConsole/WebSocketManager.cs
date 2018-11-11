using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using SuperSocket.ClientEngine;

namespace DCETestConsole
{
    public interface IWebSocketDataProcessor
    {
        void ProcessData(string data);
    }

    public class WebSocketManager
    {
        private WebSocket webSocket;
        private IWebSocketDataProcessor dataProcessor;

        public WebSocketManager(string webSocketUrl, IWebSocketDataProcessor dataProcessor)
        {
            if (webSocketUrl == null || dataProcessor == null)
            {
                throw new ArgumentNullException();
            }

            this.dataProcessor = dataProcessor;
            webSocket = new WebSocket(webSocketUrl);
            
            webSocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(Websocket_MessageReceived);
        }

        public void Start()
        {
            webSocket.Open();

            while (webSocket.State == WebSocketState.Connecting)
            {
                // by default webSocket4Net has AutoSendPing=true, so we need to wait until connection established
            }

            if (webSocket.State != WebSocketState.Open)
            {
                throw new ApplicationException($"Connection is not opened: {webSocket}");
            }
        }

        public void Stop()
        {
            if (webSocket != null)
            {
                webSocket.Close();
            }                
        }

        public void Send(string message)
        {
            webSocket.Send(message);
        }

        private void Websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            dataProcessor.ProcessData(e.Message);
        }
    }
}
