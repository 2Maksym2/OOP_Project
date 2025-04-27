using DealHub;
using DealHubWPF.Model;
using DealHubWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class AdminUsersListVM:Utilities.ViewModelBase
    {
        private Admin Admin;
        private List<RegisteredUser> _users;
        public List<RegisteredUser> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        private DealHubSystem _system;

        public ICommand AdminPanelCommand { get; }
        public AdminUsersListVM(NavigationVM navigation, DealHubSystem system)
        {
            _system = system;
            Admin = navigation.Admin;
            AdminPanelCommand = navigation.AdminPanelCommand;
            Users = system.Users;
        }

        public ICommand BanUserCommand => new RelayCommand(obj =>
        {
            try
            {
                if (obj is RegisteredUser user)
                {
                    var blockDateWindow = new BanDate();
                    if (blockDateWindow.ShowDialog() == true)
                    {
                        if (blockDateWindow.SelectedDate.HasValue)
                        {
                            Admin.BanUser(_system, user.Nickname, blockDateWindow.SelectedDate.Value);
                            _system.SaveData();
                            Users = new List<RegisteredUser>(_system.Users);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка. {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
    }
}
