using DealHub;
using DealHubWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;

namespace DealHubWPF.ViewModel
{
    class OrderVM : ViewModelBase
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public Ad SelectedAd { get; }
        public RegisteredUser AdOwner;
        private string _ownerNickname;
        public string OwnerNickname
        {
            get => _ownerNickname;
            set
            {
                _ownerNickname = value;
                OnPropertyChanged();
            }
        }
        private readonly NavigationVM _navigation;
        private readonly DealHubSystem _system;

        private RegisteredUser currentuser;

        public OrderVM(NavigationVM navigation, Ad ad, DealHubSystem system, RegisteredUser currentUser)
        {
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
            _navigation = navigation;
            _system = system;
            SelectedAd = ad;
            Image = ad.Image;
            Title = ad.Title;
            Price = ad.Price;
            OwnerNickname = ad.OwnerNickname;
            Description = ad.Description;
            currentuser = currentUser;
            AdOwner = system.Users.First(a => ad.OwnerNickname == a.Nickname);
            navigation.AnotherRegisteredUser = AdOwner;
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
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        private string _surName;
        public string SurName
        {
            get => _surName;
            set
            {
                _surName = value;
                OnPropertyChanged();
            }
        }
        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }
        private string _number;
        public string PhoneNumber
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        public ICommand SendOrder => new RelayCommand(obj =>
        {

            try
            {
                if (string.IsNullOrEmpty(PhoneNumber) || !Regex.IsMatch(PhoneNumber, @"^\d{9}$"))
                    throw new Exception("Введіть коректний номер у форматі (+380)XXXXXXXXX.");
                if (Name == null || SurName == null || Patronymic == null || Address == null)
                    throw new Exception("Заповніть усі поля");

                string contactDetails = "Мої контактні дані для зв'яку: " + "\n" + "ПІБ: " + SurName + "  " + Name + "  " + Patronymic + "\n" + "Мій номер: +380 " + PhoneNumber + "\n" + "Адреса доставки: " + Address;
                currentuser.OrderAd(SelectedAd, contactDetails, AdOwner);
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
