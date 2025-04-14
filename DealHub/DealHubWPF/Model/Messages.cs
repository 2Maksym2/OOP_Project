using DealHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHubWPF.Model
{
    public class MessageViewModel
    {
        public Message Message { get; }
        public bool IsFromCurrentUser { get; }

        public MessageViewModel(Message message, string currentUserNickname)
        {
            Message = message;
            IsFromCurrentUser = message.senderNickname == currentUserNickname;
        }

        // Для зручності доступу
        public string Content => Message.Content;
        public string SenderNickname => Message.senderNickname;
        public string ReceiverNickname => Message.receiverNickname;
        public DateTime SentAt => Message.SentAt;
    }
}
