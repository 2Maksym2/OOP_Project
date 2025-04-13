using DealHub;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHubWPF.Model
{
    public class MainAdsView
    {
        public ObservableCollection<AdsView> Ads { get; set; }

        public MainAdsView()
        {
            var rawAds = GetRealAds(); // List<Ad>
            Ads = new ObservableCollection<AdsView>(rawAds.Select(AdsView.FromAd));
        }

        public List<Ad> GetRealAds()
        {
            return new List<Ad>
        {
            new Ad("Ноутбук", "Потужний ноут", Category.Електроніка, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcSbd9N7fxcdZz511S2e1bd_IPzr3UBkbf-RVcMogznNE-POjR0HZ4EEx3DVljNQnjRSKk6bdYmyXpt93vCeoD0MdckTtVUudLQ_NmR6BCh3aHkAX3YogvMQy51NYpQb9fUsyzYzzvMdyg&usqp=CAc", 17000, "user123"),
            new Ad("Телефон", "Новий", Category.Електроніка, "https://journal.ilounge.ua/files/uploads/new_4/iphone-15-plus-category-01.webp", 8500, "user456")
        };
        }
    }
}
