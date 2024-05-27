using System;
using System.Net.Sockets;
using PrydwenCliente.classes;

namespace prydwenclient.Classes
{
    public class ChatClient(string serverIP, int port, string nickname)
    {
        private readonly string serverIP = serverIP;
        private readonly int port = port;
        private readonly TcpClient clientSocket = new();
        private readonly string nickname = nickname;
        private ServerCommunicator? communicator;

        public bool Connect()
        {
            if (IsValidNickname(this.nickname))
            {
                try
                {
                    Console.WriteLine("Conectando ao servidor...");
                    clientSocket.Connect(serverIP, port);

                    Console.WriteLine("Conectado ao servidor!");
                    this.communicator = new ServerCommunicator(clientSocket);

                    // Enviar mensagem de autenticação
                    SendAuthentication();

                    Thread receiveThread = new(ReceiveMessages);
                    receiveThread.Start();

                    SendMessages();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nickname inválido. Deve ter entre 4 e 32 caracteres e não pode começar ou terminar com espaço.");
                return false;
            }
        }

        private static bool IsValidNickname(string nickname)
        {
            return nickname.Length >= 4 && nickname.Length <= 32 && !nickname.StartsWith(' ') && !nickname.EndsWith(' ');
        }

        private void SendAuthentication()
        {
            if (communicator == null)
            {
                Console.WriteLine("Erro: Communicator não está inicializado.");
                return;
            }

            Message authMessage = new()

            {
                Type = "auth",
                Nickname = this.nickname,
                Topic = "",
                Content = ""
            };

            communicator.SendMessage(authMessage);
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    if (communicator == null)
                    {
                        Console.WriteLine("Erro: Communicator não está inicializado.");
                        return;
                    }

                    Message? message = communicator.ReceiveMessage();
                    if (message != null)
                    {
                        Console.WriteLine($"[{message.Topic}] {message.Nickname}: {message.Content}");
                    }
                    else
                    {
                        Console.WriteLine("Mensagem nula recebida.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro no recebimento: " + e.Message);
            }
        }

        private void SendMessages()
        {
            try
            {
                while (true)
                {
                    Console.Write("Digite o tópico e a mensagem (ex: #geral Olá a todos): ");
                    string? input = Console.ReadLine();

                    if (input == null)
                    {
                        Console.WriteLine("Entrada inválida. Tente novamente.");
                        continue;
                    }

                    string[] parts = input.Split([' '], 2);

                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Formato inválido. Use: <tópico> <mensagem>");
                        continue;
                    }

                    string topic = parts[0];
                    string content = parts[1];

                    if (communicator == null)
                    {
                        Console.WriteLine("Erro: Communicator não está inicializado.");
                        return;
                    }

                    Message message = new()

                    {
                        Type = "message",
                        Nickname = this.nickname,
                        Topic = topic,
                        Content = content
                    };

                    communicator.SendMessage(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro no envio: " + e.Message);
            }
        }

    }

}