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

    class ProfileVM : ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand ReviewsCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand RegUserPageCommand { get; }
        public ICommand AddAdCommand { get; }
        public ICommand UpdateAdCommand { get; }

        private NavigationVM _navigation;
        public DealHubSystem _system;
        private RegisteredUser _currentuser;
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
        public ProfileVM(NavigationVM navigation, DealHubSystem system, RegisteredUser user)
        {
            _system = system;
            _navigation = navigation;
            _currentuser = user;
            HomeCommand = navigation.HomeCommand;
            RegUserPageCommand = navigation.RegisteredUserPCommand;
            ChatsCommand = navigation.ChatsCommand;
            ReviewsCommand = navigation.ReviewsCommand;
            AddAdCommand = navigation.AddAdCommand;
            UpdateAdCommand = navigation.UpdateAdCommand;
            UserName = user.Nickname;
            Ads = _currentuser.ViewAds(_system);
        }
        private string _adName;
        public string AdName
        {
            get => _adName;
            set
            {
                _adName = value;
                OnPropertyChanged();
                SelectNameCommand.Execute(null);
            }
        }
        public ICommand DeleteAdCommand => new RelayCommand(obj =>
        {
            if (obj is Ad ad)
                _currentuser.DeleteAd(_system, ad);
            Ads = _currentuser.ViewAds(_system);
            _system.SaveData();
        });

        public ICommand SelectNameCommand => new RelayCommand(obj =>
        {
            Ads = _currentuser.Search(null, AdName, _system).Where(a => a.OwnerNickname == UserName).ToList();
        });
        public ICommand Quit => new RelayCommand(obj =>
        {
            _navigation.RegisteredUserToPass = null;
            _navigation.ad = null;
            _navigation.AnotherRegisteredUser = null;
            HomeCommand.Execute(null);
        });
        public ICommand AdUpdateCommand => new RelayCommand(obj =>
        {
            if (obj is Ad _ad)
                _navigation.ad = _ad;
            UpdateAdCommand.Execute(null);
        });

    }
}
