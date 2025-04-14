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
    class RegisteredUserPageVM:Utilities.ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand AdPageCommand { get; }
        public RegisteredUser CurrentUser { get; }
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
        public DealHubSystem _system { get; }


        public RegisteredUserPageVM(NavigationVM navigation, DealHubSystem system, RegisteredUser user)
        {
            _system = system;
            CurrentUser = user;
            HomeCommand = navigation.HomeCommand;
            ChatsCommand = navigation.ChatsCommand;
            ProfileCommand = navigation.ProfileCommand;
            AdPageCommand = navigation.AdPageCommand;
            Ads = system.AllAds.Where(a => a.IsActive == true && a.OwnerNickname!=user.Nickname).ToList();
        }
        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
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

        public ICommand SelectNameCommand => new RelayCommand(obj =>
        {
            Ads = CurrentUser.Search(SelectedCategory, AdName, _system).Where(a=> a.OwnerNickname != CurrentUser.Nickname).ToList();
        });


        public ICommand SelectCategoryCommand => new RelayCommand(obj =>
        {
            if (obj is Category category)
                SelectedCategory = category;
            else SelectedCategory = null;
            Ads = CurrentUser.Search(SelectedCategory, AdName, _system).Where(a=> a.OwnerNickname != CurrentUser.Nickname && a.IsActive == true).ToList();
        });

    }

}
