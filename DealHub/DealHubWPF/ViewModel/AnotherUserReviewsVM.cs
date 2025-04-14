using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class AnotherUserReviewsVM
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand AnotherUserCommand { get; }


        public AnotherUserReviewsVM(NavigationVM navigation)
        {
            AnotherUserCommand = navigation.AnotherUserCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
        }

    }
}
