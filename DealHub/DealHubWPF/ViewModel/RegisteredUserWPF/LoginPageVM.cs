using DealHub;
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
    class LoginPageVM : ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand RegisterPageCommand { get; }
        public ICommand RegUserCommand { get; }
        public ICommand AdminPanelCommand { get; }

        public User? currentUser { get; set; } = null;
        public DealHubSystem _system;
        private readonly NavigationVM _navigation;

        public LoginPageVM(NavigationVM navigation, DealHubSystem system)
        {
            HomeCommand = navigation.HomeCommand;
            RegisterPageCommand = navigation.RegisterPageCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
            AdminPanelCommand = navigation.AdminPanelCommand;
            _system = system;
            _navigation = navigation;
        }
        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set { _nickname = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public User LoginUser()
        {
            if (string.IsNullOrWhiteSpace(Nickname) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            try
            {
                return currentUser = _system.Login(Nickname, Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }

        public ICommand LoginAndGoCommand => new RelayCommand(obj => LoginAndNavigate());

        private void LoginAndNavigate()
        {
            User? user = LoginUser();
            if (user != null && user is RegisteredUser reguser)
            {
                _navigation.RegisteredUserToPass = reguser;
                RegUserCommand.Execute(null);
            }
            else if (user is Admin admin)
            {
                _navigation.Admin = admin;
                AdminPanelCommand.Execute(null);
            }

        }

    }
}
