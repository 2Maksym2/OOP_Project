using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DealHub.IManageAds;

namespace DealHub
{
    public class RegisteredUser : User, IManageAds
    {
        private DateTime blockedTime;
        public DateTime BlockedTime
        {
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }
        private List<Review> reviews = new List<Review>();
        private List<Ad> ads = new List<Ad>();
        private List<Message> messagesSent = new List<Message>();
        private List<Message> messagesReceived = new List<Message>();


        public static event Action<string> MessageForUser;
        public List<Ad> Ads => ads;
        public List<Message> MessagesSent => messagesSent;
        public List<Message> MessagesReceived => messagesReceived;
        public List<Review> Reviews => reviews;


        public RegisteredUser(string nickname, string password) : base(nickname, password) { }


        public List<Ad> ViewMyAds()
        {
            throw new NotImplementedException();
        }
        public void OrderAd(Ad ad, string ContactDetails, RegisteredUser owner)
        {
            throw new NotImplementedException();
        }
        public void CreateAd(DealHubSystem system, string title, string description, Category category, string image, double price, RegisteredUser user)
        {
            throw new NotImplementedException();
        }
        public void EditAd(int adId, string newTitle, string newDescription)
        {
            throw new NotImplementedException();
        }
        public void DeleteAd(DealHubSystem system, int adId)
        {
            throw new NotImplementedException();
        }
        public void SendMessage(RegisteredUser receiver, string content)
        {
            throw new NotImplementedException();
        }
        public List<User> ViewChats(int n, DealHubSystem system)
        {
            throw new NotImplementedException();
        }
        public void ViewMessages(User receiver, User sender)
        {
            throw new NotImplementedException();
        }
        public void LeaveReview(RegisteredUser receiver, string content)
        {
            throw new NotImplementedException();
        }
        public void ViewReviews()
        {
            throw new NotImplementedException();
        }
        public Complaint SendComplaint(DealHubSystem system, RegisteredUser? receiver, Ad? receiverAd, string description)
        {
            throw new NotImplementedException();
        }
        public override void ShowMenu()
        {
            throw new NotImplementedException();
        }

    }
}
