using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DealHub;
using DealHubWPF.Utilities;

namespace DealHubWPF.ViewModel
{
    class AnotherUserVM:Utilities.ViewModelBase
    {
        public ICommand ReviewsCommand { get; }
        public ICommand RegUserPageCommand { get; }
        public ICommand AddAdCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand AdPageCommand { get; }

        public DealHubSystem _system;
        public RegisteredUser _selectedUser;
        public NavigationVM _navigation;
        private List<Ad> _ads;
        public List<Ad> Ads
        {
            get => _ads;
            set
            {
                _ads = value;
                OnPropertyChanged();
            }
        }
        private string _userName;
        public string UserName
        {
            get => _userName;
            private set => _userName = value;
        }

        public AnotherUserVM(NavigationVM navigation, DealHubSystem system, RegisteredUser SelectedUser)
        {
            _system = system;
            _selectedUser = SelectedUser;
            _navigation = navigation;
            UserName = _selectedUser.Nickname;
            RegUserPageCommand = navigation.RegisteredUserPCommand;
            ReviewsCommand = navigation.AnotherUserReviewsCommand;
            AddAdCommand = navigation.AddAdCommand;
            ChatsCommand = navigation.ChatsCommand;
            AdPageCommand = navigation.AdPageCommand;
            ProfileCommand = navigation.ProfileCommand;
            Ads = system.AllAds.Where(a => a.OwnerNickname == UserName && a.IsActive == true).ToList();
        }

        public ICommand GoAdPageCommand => new RelayCommand(obj =>
        {
            if (obj is Ad selectedAd)
                _navigation.ad = selectedAd;
            AdPageCommand.Execute(null);
        });

    }
}
