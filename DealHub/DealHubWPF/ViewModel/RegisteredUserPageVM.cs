using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class RegisteredUserPageVM
    {
        public ICommand HomeCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }

        public RegisteredUserPageVM(NavigationVM navigation)
        {
            HomeCommand = navigation.HomeCommand;
            ChatsCommand = navigation.ChatsCommand;
            ProfileCommand = navigation.ProfileCommand;
        }
    }

}
