using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.API.Implemetations
{
    public class WebSocketClient
    {
        private readonly ClientWebSocket _webSocket;

        public WebSocketClient()
        {
            _webSocket = new ClientWebSocket();
        }

        public async Task ConnectAsync(string uri)
        {
            await _webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
        }

        public async Task<string> ReceiveMessageAsync()
        {
            var buffer = new byte[1024 * 4];
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

        public async Task SendMessageAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task CloseAsync()
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        }
    }
}
