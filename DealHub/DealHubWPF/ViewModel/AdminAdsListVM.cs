using DealHub;
using DealHubWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
namespace DealHubWPF.ViewModel
{
    class AdminAdsListVM:Utilities.ViewModelBase
    {
        private Admin Admin;
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
        private DealHubSystem _system;

        public ICommand AdminPanelCommand { get; }
        public AdminAdsListVM(NavigationVM navigation, DealHubSystem system)
        {
            _system = system;
            Admin = navigation.Admin;
            AdminPanelCommand = navigation.AdminPanelCommand;
            Ads = Admin.ViewAds(_system);
        }

        public ICommand DeleteAdCommand => new RelayCommand(obj =>
        {
            try
            {
                if (obj is Ad ad)
                {
                    Admin.DeleteAd(_system, ad);
                    _system.SaveData();
                    Ads = new List<Ad>(_system.AllAds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка. {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

    }
}
