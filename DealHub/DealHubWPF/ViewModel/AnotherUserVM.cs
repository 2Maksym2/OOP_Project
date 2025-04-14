using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class AnotherUserVM
    {
        public ICommand ReviewsCommand { get; }
        public ICommand RegUserPageCommand { get; }
        public ICommand AddAdCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand ProfileCommand { get; }


        public AnotherUserVM(NavigationVM navigation)
        {
            RegUserPageCommand = navigation.RegisteredUserPCommand;
            ReviewsCommand = navigation.AnotherUserReviewsCommand;
            AddAdCommand = navigation.AddAdCommand;
            ChatsCommand = navigation.ChatsCommand;
            ProfileCommand = navigation.ProfileCommand;
        }

    }
}
