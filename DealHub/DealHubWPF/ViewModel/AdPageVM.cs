using DealHub;
using DealHubWPF.Model;
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
    class AdPageVM:Utilities.ViewModelBase
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand AnotherUserCommand { get; }
        public ICommand OrderCommand { get; }
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
        private int _reviewsCount;
        public int ReviewsCount
        {
            get => _reviewsCount;
            set
            {
                _reviewsCount = value;
                OnPropertyChanged();
            }
        }
        private readonly NavigationVM _navigation;
        private readonly DealHubSystem _system;

        private RegisteredUser currentuser;
        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set
            {
                _messageContent = value;
                OnPropertyChanged();
            }
        }

        public AdPageVM(NavigationVM navigation, Ad ad, DealHubSystem system, RegisteredUser currentUser)
        {
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
            AnotherUserCommand = navigation.AnotherUserCommand;
            OrderCommand = navigation.OrderPageCommand;
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
            ReviewsCount = AdOwner.Reviews.Count();
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
        private List<MessageViewModel> _chatMessagesForUI;
        public List<MessageViewModel> ChatMessagesForUI
        {
            get => _chatMessagesForUI;
            set
            {
                _chatMessagesForUI = value;
                OnPropertyChanged();
            }
        }

        public void LoadMessages()
        {
            if (AdOwner != null)
            {
                _navigation.AnotherRegisteredUser = AdOwner;
                var allMessages = currentuser.GetChatMessages(AdOwner, currentuser);
                ChatMessagesForUI = allMessages.Select(m => new MessageViewModel(m, currentuser.Nickname)).ToList();
            }
        }
        public ICommand SendMessageCommand => new RelayCommand(obj =>
        {
            try
            {
                currentuser.SendMessage(AdOwner, MessageContent);
                _system.SaveData();
                LoadMessages();
                MessageContent = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
        public ICommand SendR => new RelayCommand(obj =>
        {
            var ReviewWindow = new View.SendReview(_navigation, _system);
            ReviewWindow.ShowDialog();
        });
        public ICommand SendC => new RelayCommand(obj =>
        {
            var ComplaintWindow = new View.SendComplaint(_navigation, _system, SelectedAd);
            ComplaintWindow.ShowDialog();
        });
        public ICommand Order => new RelayCommand(obj =>
        {
            if (_navigation.ad.IsActive == true)
            {
                OrderCommand.Execute(null);
            }
            else
            {
                MessageBox.Show("Товар недоступний", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        });

    }
}
