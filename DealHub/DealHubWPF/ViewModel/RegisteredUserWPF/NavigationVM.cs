using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHubWPF.Utilities;
using System.Windows.Input;
using DealHub;
using System.Windows;
using DealHubWPF.ViewModel;

namespace DealHubWPF.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        public Ad ad;
        private readonly DealHubSystem _system;
        private object _currentView;
        public RegisteredUser? RegisteredUserToPass { get; set; }
        public RegisteredUser? AnotherRegisteredUser { get; set; }
        public Admin? Admin { get; set; }

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
        public ICommand AddAdCommand { get; set; }
        public ICommand AnotherUserCommand { get; set; }
        public ICommand AnotherUserReviewsCommand { get; set; }
        public ICommand AdPageCommand { get; set; }
        public ICommand OrderPageCommand { get; set; }
        public ICommand UpdateAdCommand { get; set; }
        public ICommand AdminPanelCommand { get; set; }
        public ICommand AdminUsersListCommand { get; set; }
        public ICommand AdminAdsListCommand { get; set; }
        public ICommand AdminComplaintsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(this, _system);
        private void Profile(object obj) => CurrentView = new ProfileVM(this, _system, RegisteredUserToPass);
        public void RegisterPage(object obj) => CurrentView = new RegisterPageVM(this, _system);
        public void LoginPage(object obj) => CurrentView = new LoginPageVM(this, _system);
        public void RegisteredUserPage(object obj)
        {
            if (RegisteredUserToPass != null)
            {
                CurrentView = new RegisteredUserPageVM(this, _system, RegisteredUserToPass);
            }
            else
            {
                MessageBox.Show("Користувач не переданий!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void Chats(object obj) => CurrentView = new ChatsVM(this, _system, RegisteredUserToPass);
        public void Rewies(object obj) => CurrentView = new ReviewsVM(this, RegisteredUserToPass);
        public void AddAd(object obj) => CurrentView = new AddAdVM(this, RegisteredUserToPass, _system);
        public void AnotherUser(object obj) => CurrentView = new AnotherUserVM(this, _system, AnotherRegisteredUser);
        public void AnotherUserReviews(object obj) => CurrentView = new AnotherUserReviewsVM(this, AnotherRegisteredUser);
        public void AdPage(object obj) => CurrentView = new AdPageVM(this, ad, _system, RegisteredUserToPass);
        public void Order(object obj) => CurrentView = new OrderVM(this, ad, _system, RegisteredUserToPass);
        public void UpdateAd(object obj) => CurrentView = new UpdateAdVM(this, ad, _system, RegisteredUserToPass);
        public void AdminPanel(object obj) => CurrentView = new AdminPanelVM(this, _system);
        public void AdminUsersList(object obj) => CurrentView = new AdminUsersListVM(this, _system);
        public void AdminAdsList(object obj) => CurrentView = new AdminAdsListVM(this, _system);
        public void AdminComplaints(object obj) => CurrentView = new AdminComplaintsVM(this, _system);

        public NavigationVM(DealHubSystem system)
        {
            //головна система
            _system = system;
            //звичайні користувачі
            HomeCommand = new RelayCommand(Home);
            ProfileCommand = new RelayCommand(Profile);
            RegisterPageCommand = new RelayCommand(RegisterPage);
            LoginPageCommand = new RelayCommand(LoginPage);
            RegisteredUserPCommand = new RelayCommand(RegisteredUserPage);
            ChatsCommand = new RelayCommand(Chats);
            ReviewsCommand = new RelayCommand(Rewies);
            AddAdCommand = new RelayCommand(AddAd);
            AdPageCommand = new RelayCommand(AdPage);
            AnotherUserCommand = new RelayCommand(AnotherUser);
            AnotherUserReviewsCommand = new RelayCommand(AnotherUserReviews);
            OrderPageCommand = new RelayCommand(Order);
            UpdateAdCommand = new RelayCommand(UpdateAd);
            //Адміністратор
            AdminUsersListCommand = new RelayCommand(AdminUsersList);
            AdminAdsListCommand = new RelayCommand(AdminAdsList);
            AdminComplaintsCommand = new RelayCommand(AdminComplaints);
            AdminPanelCommand = new RelayCommand(AdminPanel);
            //Початкова сторінка
            CurrentView = new HomeVM(this, _system);
        }

    }
}
