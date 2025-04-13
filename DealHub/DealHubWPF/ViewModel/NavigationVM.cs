using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHubWPF.Utilities;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class NavigationVM:ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            set
            {
                _currentView = value; OnPropertyChanged();
            }
            get { return _currentView; }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand RegisterPageCommand { get; set; }
        public ICommand LoginPageCommand { get; set; }
        public ICommand RegisteredUserPCommand { get; set; }
        public ICommand ChatsCommand { get; set; }
        public ICommand ReviewsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(this);
        private void Profile(object obj) => CurrentView = new ProfileVM(this);
        public void RegisterPage(object obj) => CurrentView = new RegisterPageVM(this);
        public void LoginPage(object obj) => CurrentView = new LoginPageVM(this);
        public void RegisteredUserPage(object obj) => CurrentView = new RegisteredUserPageVM(this);
        public void Chats(object obj) => CurrentView = new ChatsVM(this);
        public void Rewies(object obj) => CurrentView = new ReviewsVM(this);

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            ProfileCommand = new RelayCommand(Profile);
            RegisterPageCommand = new RelayCommand(RegisterPage);
            LoginPageCommand = new RelayCommand(LoginPage);
            RegisteredUserPCommand = new RelayCommand(RegisteredUserPage);
            ChatsCommand = new RelayCommand(Chats);
            ReviewsCommand = new RelayCommand(Rewies);

            //Startup Page
            CurrentView = new HomeVM(this);
        }
        
    }
}
