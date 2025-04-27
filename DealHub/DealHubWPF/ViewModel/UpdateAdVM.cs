using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DealHub;
using DealHubWPF.Utilities;
using System.Windows; 

namespace DealHubWPF.ViewModel
{
    class UpdateAdVM:Utilities.ViewModelBase
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }

        private Ad _ad;
        private DealHubSystem _system;
        private NavigationVM _navigation;
        private RegisteredUser _currentUser;
        public IEnumerable<Category> Categories
        {
            get
            {
                return Enum.GetValues(typeof(Category)).Cast<Category>();
            }
        }

        public UpdateAdVM(NavigationVM navigation, Ad ad, DealHubSystem system, RegisteredUser currentUser)
        {
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;

            _system = system;
            _ad = ad;
            _navigation = navigation; 
            _currentUser = currentUser;

            Image = ad.Image;
            Title = ad.Title;
            Price = ad.Price;
            SelectedCategory = ad.Category;
            Description = ad.Description;
            IsActive = ad.IsActive;
        }
        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }


        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public ICommand AdUpdateCommand => new RelayCommand(obj =>
        {
            try
            {

                _currentUser.EditAd(_ad, Title, Description, SelectedCategory, Image, Price, IsActive);
                _system.SaveData();
                ProfileCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

    }
}
