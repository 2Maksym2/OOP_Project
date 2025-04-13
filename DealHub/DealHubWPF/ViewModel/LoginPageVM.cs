using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
     class LoginPageVM
    {
        public ICommand HomeCommand { get; }
        public ICommand RegisterPageCommand { get; }
        public ICommand RegUserCommand { get; }

        public LoginPageVM(NavigationVM navigation)
        {
            HomeCommand = navigation.HomeCommand;
            RegisterPageCommand = navigation.RegisterPageCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;

        }

    }
}
