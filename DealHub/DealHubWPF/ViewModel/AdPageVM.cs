using DealHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class AdPageVM
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand AnotherUserCommand { get; }
        public ICommand OrderCommand { get; }
        public Ad SelectedAd { get; }

        public AdPageVM(NavigationVM navigation, Ad ad)
        {
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
            AnotherUserCommand = navigation.AnotherUserCommand;
            SelectedAd = ad;


        }

    }
}
