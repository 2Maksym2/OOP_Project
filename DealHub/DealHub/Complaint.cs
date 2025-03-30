using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DealHub
{
    public class Complaint
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        [JsonIgnore]
        public RegisteredUser? Receiver { get; }
        public string ReceiverName { get; set; }
        [JsonIgnore]
        public Ad? ReceiverAd { get; }
        public int AdId { get; set; }
        public string AdTitle { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public Complaint() { }
        public Complaint(RegisteredUser complainant, RegisteredUser? receiver, Ad? receiverAd, string description)
        {
            Id = _nextId++;
            ReceiverName = receiver.Nickname;
            Title = "Скарга";
            Description = description;
            if (receiverAd != null)
            {
                AdTitle = receiverAd.Title;
                AdId = receiverAd.Id;
            }
        }
        public static void UpdateTotalComplaintsCreated(List<Complaint> complaints)
        {
            if (complaints.Any())
            {
                _nextId = complaints.Max(a => a.Id) + 1;
            }
        }

    }
}
