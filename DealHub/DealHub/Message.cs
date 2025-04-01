using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DealHub
{
    public class Message
    {
        public string receiverNickname { get; set; }
        public string senderNickname { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;

        public Message() { }
        public Message(string sender, string receiver, string content)
        {
            senderNickname = sender;
            receiverNickname = receiver;
            Content = content;
        }
    }
}
