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
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("Назва не може бути порожньою або складатися тільки з пробілів!");
                title = value;
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("Опис не може бути порожнім або складатися тільки з пробілів!");
                description = value;
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new Exception("Ціна не може бути меншою за 0 грн");
                price = value;
            }
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public string OwnerNickname { get; set; }
        [JsonIgnore] //Щоб уникнути зациклення
        public RegisteredUser Owner { get; private set; }



        public Ad() { }
        public Ad(string title, string description, Category category, string image, double adprice, RegisteredUser user)
        {
            Id = totalAdsCreated++;
            Title = title;
            Description = description;
            Category = category;
            Image = image;
            OwnerNickname = user.Nickname;
            Price = adprice;
        }


        // Метод для оновлення ID при завантаженні даних
        public static void UpdateTotalAdsCreated(List<Ad> ads)
        {
            if (ads.Any())
            {
                totalAdsCreated = ads.Max(a => a.Id) + 1;
            }
        }
    }
}

