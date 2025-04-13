using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{

    class ProfileVM
    {
        public ICommand HomeCommand { get; }
        public ICommand ReviewsCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand RegUserPageCommand { get; }

        public ProfileVM(NavigationVM navigation)
        {
            HomeCommand = navigation.HomeCommand;
            RegUserPageCommand = navigation.RegisteredUserPCommand;
            ChatsCommand = navigation.ChatsCommand;
            ReviewsCommand = navigation.ReviewsCommand;
        }
    }
}
