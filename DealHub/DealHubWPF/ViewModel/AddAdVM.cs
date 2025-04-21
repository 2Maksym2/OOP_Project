using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DealHub;
using DealHubWPF.Utilities;
namespace DealHubWPF.ViewModel
{
    class AddAdVM:Utilities.ViewModelBase
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public DealHubSystem _system;
        public RegisteredUser _currentuser;
        private string _userName;
        public string UserName
        {
            get => _userName;
            private set => _userName = value;
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
        public IEnumerable<Category> Categories
        {
            get
            {
                return Enum.GetValues(typeof(Category)).Cast<Category>();
            }
        }
        public AddAdVM(NavigationVM navigation, RegisteredUser currentUser, DealHubSystem system)
        {
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
            _system = system;
            _currentuser = currentUser;
            UserName = _currentuser.Nickname;
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
        public ICommand AdAddCommand => new RelayCommand(obj =>
        {
            try
            {

                _currentuser.CreateAd(_system, Title, Description, SelectedCategory, Image, Price, _currentuser);
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
