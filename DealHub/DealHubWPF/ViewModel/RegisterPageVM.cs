using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class RegisterPageVM
    {

        public ICommand HomeCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand RegUserCommand { get; }

        public RegisterPageVM(NavigationVM navigation)
        {
            HomeCommand = navigation.HomeCommand;
            LoginCommand = navigation.LoginPageCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;

        }

    }
}
