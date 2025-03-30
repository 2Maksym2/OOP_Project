using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub
{
    public class Admin : User
    {
        public static event Action<string> MessageForUser;
        public Admin(string nickname, string password) : base(nickname, password)
        {
            throw new NotImplementedException();
        }

        public bool ViewComplaints(DealHubSystem system)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComplaint(DealHubSystem system, int complaintId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAd(DealHubSystem system, int adId)
        {
            throw new NotImplementedException();
        }
        public void BanUser(DealHubSystem system, string username, DateTime EndBan)
        {
            throw new NotImplementedException();
        }
        public override void ShowMenu()
        {
            throw new NotImplementedException();
        }
    }
}
