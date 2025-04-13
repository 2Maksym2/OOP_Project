using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class ChatsVM
    {
        public ICommand HomeCommand { get; }
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ReviewsCommand { get; }

        public ChatsVM(NavigationVM navigation)
        {
            HomeCommand = navigation.HomeCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ReviewsCommand = navigation.ReviewsCommand;
        }

    }
}
