using DealHub;
using DealHubWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class AdminPanelVM : ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand AdminAdsListCommand { get; }
        public ICommand AdminUsersListCommand { get; }
        public ICommand AdminComplaintsCommand { get; }

        public DealHubSystem _system;
        private readonly NavigationVM _navigation;
        public AdminPanelVM(NavigationVM navigation, DealHubSystem system)
        {
            _system = system;
            _navigation = navigation;
            HomeCommand = navigation.HomeCommand;
            AdminAdsListCommand = navigation.AdminAdsListCommand;
            AdminComplaintsCommand = navigation.AdminComplaintsCommand;
            AdminUsersListCommand = navigation.AdminUsersListCommand;
        }
        public ICommand Quit => new RelayCommand(obj =>
        {
            HomeCommand.Execute(null);
            _navigation.Admin = null;
        });

    }
}
