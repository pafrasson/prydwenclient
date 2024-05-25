using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PrydwenCliente.Classes
{
    public class Cliente
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private string _nickname;
        private const int Port = 2077;

        public Cliente(string serverIp, string nickname)
        {
            _client = new TcpClient(serverIp, Port);
            _stream = _client.GetStream();
            _nickname = nickname;
        }
        public void Iniciar()
        {
            
        }
    }
}