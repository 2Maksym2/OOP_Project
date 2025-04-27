using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DealHub
{
    public class Guest : User
    {
        public Guest() { }
        public override List<Ad> ViewAds(DealHubSystem system)
        {
            return system.AllAds.Where(m=> m.IsActive == true).ToList();
        }
    }
}
