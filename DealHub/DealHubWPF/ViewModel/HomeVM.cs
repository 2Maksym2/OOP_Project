using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DealHubWPF.Model;

namespace DealHubWPF.ViewModel
{
    class HomeVM:Utilities.ViewModelBase
    {
        public ICommand RegisterPageCommand { get; }
        public HomeVM(NavigationVM navigation)
        {
            RegisterPageCommand = navigation.RegisterPageCommand;
        }
    }
}
