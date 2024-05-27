using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrydwenCliente.classes
{
    public class Message
    {
        public required string Type { get; set; }
        public required string Nickname { get; set; }
        public required string Topic { get; set; }
        public required string Content { get; set; }
    }


}