using System;
using prydwenclient.Classes;

string? serverIP = null;
string? nickname = null;

while (string.IsNullOrEmpty(serverIP))
{
    Console.Write("Digite o endereço IP do servidor: ");
    serverIP = Console.ReadLine();

    if (string.IsNullOrEmpty(serverIP))
    {
        Console.WriteLine("Endereço IP não pode ser vazio. Tente novamente.");
    }
}

while (string.IsNullOrEmpty(nickname))
{
    Console.Write("Digite seu nickname: ");
    nickname = Console.ReadLine();

    if (string.IsNullOrEmpty(nickname))
    {
        Console.WriteLine("Nickname não pode ser vazio. Tente novamente.");
    }
}

ChatClient client = new(serverIP, 2077, nickname);
client.Connect();
