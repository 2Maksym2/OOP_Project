using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DealHub
{
    public class DealHubSystem
    {
        private const string UsersFile = "users.json";
        private const string AdminsFile = "admins.json";
        private const string ComplaintsFile = "complaints.json";

        private List<RegisteredUser> users = new();
        private List<Admin> admins = new();
        private List<Complaint> complaints = new();
        private List<Ad> allAds = new();

        public List<Ad> AllAds => allAds;
        public List<RegisteredUser> Users => users;
        public List<Admin> Admins => admins;
        public List<Complaint> Complaints => complaints;

        public static event Action<string> MessageForUser;


        public void AddComplaint(Complaint complaint)
        {
            throw new NotImplementedException();
        }
        public User RegisterUser(string nickname, string password, bool? isAdmin)
        {
            throw new NotImplementedException();
        }
        public User? Login(string nickname, string password)
        {
            throw new NotImplementedException();
        }
        public void LoadData()
        {
            throw new NotImplementedException();
        }
        public void SaveData()
        {
            throw new NotImplementedException();
        }
        public void UpdateUserData()
        {
            throw new NotImplementedException();
        }
    }
}

