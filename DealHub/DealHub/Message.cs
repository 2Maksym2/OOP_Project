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
        [JsonIgnore]
        public User Sender { get; private set; }
        [JsonIgnore]
        public User Receiver { get; private set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;

        public Message() { }
        public Message(User sender, User receiver, string content)
        {
            senderNickname = sender.Nickname;
            receiverNickname = receiver.Nickname;
            Content = content;
        }
    }
}
