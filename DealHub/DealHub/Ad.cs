using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DealHub
{
    public class Ad
    {
        private static int totalAdsCreated = 1;
        private string title;
        private string description;
        private double price;

        public static int TotalAdsCreated { get; }
        public int Id { get; set; }
        public string Title
        {
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }
        public string Description
        {
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }
        public double Price
        {
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }
        public Category Category { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public string OwnerNickname { get; set; }
        public RegisteredUser Owner { get; private set; }



        public Ad() { }
        public Ad(string title, string description, Category category, string image, double adprice, RegisteredUser user)
        {
            throw new NotImplementedException();
        }


        public static void UpdateTotalAdsCreated(List<Ad> ads)
        {
            throw new NotImplementedException();
        }
    }
}
