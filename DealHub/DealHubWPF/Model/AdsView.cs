using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub;
namespace DealHubWPF.Model
{
    public class AdsView
    {
        public string Title { get; set; }
        public string PriceFormatted { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public static AdsView FromAd(Ad ad) => new AdsView
        {
            Title = ad.Title,
            Description = ad.Description,
            PriceFormatted = $"{ad.Price:N1} грн",
            Image = ad.Image
        };
    }
}
