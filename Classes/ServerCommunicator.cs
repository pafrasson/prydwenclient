using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using PrydwenCliente.classes;

namespace prydwenclient.Classes

{
    public class ServerCommunicator(TcpClient clientSocket)
    {
        private readonly TcpClient clientSocket = clientSocket;

        public void SendMessage(Message message)
        {
            try
            {
                NetworkStream stream = clientSocket.GetStream();
                string jsonMessage = JsonSerializer.Serialize(message) + "\n";
                byte[] data = Encoding.ASCII.GetBytes(jsonMessage);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro no envio: " + e.Message);
            }
        }

        public Message? ReceiveMessage()
        {
            try
            {
                NetworkStream stream = clientSocket.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string jsonMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Message? message = JsonSerializer.Deserialize<Message>(jsonMessage);

                    if (message != null)
                    {
                        return message;
                    }
                    else
                    {
                        Console.WriteLine("Erro na deserialização da mensagem recebida.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro no recebimento: " + e.Message);
            }

            return null;
        }
    }

}