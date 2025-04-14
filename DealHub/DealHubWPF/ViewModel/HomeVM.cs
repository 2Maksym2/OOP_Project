using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DealHub;
using System.Net;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using DealHubWPF.Utilities;

namespace DealHubWPF.ViewModel
{
    class HomeVM:Utilities.ViewModelBase
    {
        public User? currentUser = new Guest();
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
        public ICommand RegisterPageCommand { get; }
        public ICommand AdPageCommand { get; }
        public DealHubSystem _system;

        public HomeVM(NavigationVM navigation, DealHubSystem system)
        {
            system.LoadData();
            _system = system;
            RegisterPageCommand = navigation.RegisterPageCommand;
            AdPageCommand = navigation.RegisterPageCommand;
            Ads = system.AllAds.Where(a => a.IsActive == true).ToList();
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
            Ads = currentUser.Search(SelectedCategory, AdName, _system);
        });


        public ICommand SelectCategoryCommand => new RelayCommand(obj =>
        {
            if (obj is Category category)
                SelectedCategory = category;
            else SelectedCategory = null;
            Ads = currentUser.Search(SelectedCategory, AdName, _system);
        });
    }
}
