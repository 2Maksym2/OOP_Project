using DealHub;
using DealHubWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class RegisterPageVM:Utilities.ViewModelBase
    {
        public DealHubSystem _system;
        public ICommand HomeCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand RegUserCommand { get; }
        public User? currentUser { get; set; } = null;
        private readonly NavigationVM _navigation;
        public RegisterPageVM(NavigationVM navigation, DealHubSystem system)
        {
            _system = system;
            _navigation = navigation;
            HomeCommand = navigation.HomeCommand;
            LoginCommand = navigation.LoginPageCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
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
        public RegisteredUser RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(Nickname) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            try
            {
                currentUser = _system.RegisterUser(Nickname, Password, false);
                if (currentUser is RegisteredUser regUser)
                {
                    _system.SaveData();
                    return regUser;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при реєстрації: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }
        public ICommand RegisterAndGoCommand => new RelayCommand(obj => RegisterAndNavigate());

        private void RegisterAndNavigate()
        {
            var user = RegisterUser();
            if (user != null)
            {
                _navigation.RegisteredUserToPass = user;
                RegUserCommand.Execute(null);
            }
        }
    }
}
