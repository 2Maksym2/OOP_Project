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
     class LoginPageVM:Utilities.ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand RegisterPageCommand { get; }
        public ICommand RegUserCommand { get; }
        public User? currentUser { get; set; } = null;
        public DealHubSystem _system;
        private readonly NavigationVM _navigation;

        public LoginPageVM(NavigationVM navigation, DealHubSystem system)
        {
            HomeCommand = navigation.HomeCommand;
            RegisterPageCommand = navigation.RegisterPageCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
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

        public RegisteredUser LoginUser()
        {
            if (string.IsNullOrWhiteSpace(Nickname) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            try
            {
                currentUser = _system.Login(Nickname, Password);
                if (currentUser is RegisteredUser regUser)
                {
                    _system.SaveData();
                    return regUser;
                }
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
            var user = LoginUser();
            if (user != null)
            {
                _navigation.RegisteredUserToPass = user;
                RegUserCommand.Execute(null);
            }
        }

    }
}
