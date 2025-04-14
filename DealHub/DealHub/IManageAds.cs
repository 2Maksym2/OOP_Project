using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub
{
    public interface IManageAds
    {
        void CreateAd(DealHubSystem system, string title, string description, Category category, string image, double price, RegisteredUser user);
        void EditAd(int adId, string newTitle, string newDescription);
        void DeleteAd(DealHubSystem system, Ad ad);
    }
}
