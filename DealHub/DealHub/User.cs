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
            get => nickname;
            set
            {
                if (value.Length < 4)
                    throw new ArgumentException("Нікнейм повинен містити мінімум 4 символи");
                nickname = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Пароль повинен містити мінімум 6 символів");
                password = value;
            }
        }

        public User() { Nickname = "guest"; }
        public User(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
            IsAdmin = false;
            UserCount++;
        }

        public List<Ad> Search(Category? newcategory, string newtitle, DealHubSystem system)
        {
            var ads = system.AllAds.Where(a => a.IsActive == true).ToList();

            if (newcategory.HasValue)
                ads = ads.Where(a => a.Category == newcategory.Value && a.IsActive == true).ToList(); 

            if (!string.IsNullOrWhiteSpace(newtitle))
                ads = ads.Where(a => a.Title.Contains(newtitle, StringComparison.OrdinalIgnoreCase) && a.IsActive == true).ToList(); 

            return ads;
        }

        public virtual List<Ad> ViewAds(DealHubSystem system)
        {
         return new List<Ad>();
        }
    }
}
