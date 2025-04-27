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
        void EditAd(Ad ad, string newTitle, string newDescription, Category newcategory, string newimage, double newprice, bool active);
        void DeleteAd(DealHubSystem system, Ad ad);
    }
}
