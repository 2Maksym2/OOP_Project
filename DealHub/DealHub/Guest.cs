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
        public static event Action<string> MessageForUser;
        public override void ShowMenu()
        {
            throw new NotImplementedException();
        }
    }
}
