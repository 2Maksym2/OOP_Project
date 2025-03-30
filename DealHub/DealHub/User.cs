using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DealHub
{
    public abstract class User
    {
        private string nickname;
        private string password;
        public static int UserCount { get; set; }
        public bool IsAdmin { get; protected set; } = false;
        public string Nickname
        {
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Password
        {
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }

        public User() { throw new NotImplementedException(); }
        public User(string nickname, string password)
        {
            throw new NotImplementedException();
        }

        public List<Ad> Search(Category newcategory, string newtitle, DealHubSystem system)
        {
            throw new NotImplementedException();
        }

        public virtual void ShowMenu()
        {
            throw new NotImplementedException();
        }
    }
}
