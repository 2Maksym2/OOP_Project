using DealHub;
using DealHubWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DealHubWPF.Model;

namespace DealHubWPF.ViewModel
{
    class ChatsVM : ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ReviewsCommand { get; }
        public ICommand AnotherUserCommand { get; }

        private RegisteredUser currentuser;
        private DealHubSystem _system;
        private List<User> _chats;
        private string _userName;
        public string UserName
        {
            get => _userName;
            private set => _userName = value;
        }
        private string _selectedUserName;
        public string SelectedUserName
        {
            get => _selectedUserName;
            set
            {
                _selectedUserName = value;
                OnPropertyChanged();
            }
        }
        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            private set
            {
                _selectedUser = value;
                SelectedUserName = _selectedUser.Nickname;
                OnPropertyChanged();
            }
        }

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


        public List<User> UserChats
        {
            get => _chats;
            set
            {
                _chats = value;
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
        private readonly NavigationVM _navigation;

        public ChatsVM(NavigationVM navigation, DealHubSystem system, RegisteredUser CurrentUser)
        {
            _system = system;
            currentuser = CurrentUser;
            _navigation = navigation;
            HomeCommand = navigation.HomeCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ReviewsCommand = navigation.ReviewsCommand;
            AnotherUserCommand = navigation.AnotherUserCommand;
            UserName = currentuser.Nickname;
        }
        private bool _isBuyerSelected = false;
        public bool IsBuyerSelected
        {
            get => _isBuyerSelected;
            set
            {
                _isBuyerSelected = value;
                if (IsBuyerSelected) { IsSellerSelected = false; }
                LoadChats();
                OnPropertyChanged();
            }
        }

        private bool _isSellerSelected = false;
        public bool IsSellerSelected
        {
            get => _isSellerSelected;
            set
            {
                _isSellerSelected = value;
                if (IsSellerSelected) { IsBuyerSelected = false; }
                LoadChats();
                OnPropertyChanged();
            }
        }
        private void LoadChats()
        {
            if (IsBuyerSelected)
            {
                UserChats = currentuser.ViewChats(1, _system);
            }
            else if (IsSellerSelected)
            {
                UserChats = currentuser.ViewChats(2, _system);
            }
        }

        public void LoadMessages(User _selectedUser)
        {
            if (_selectedUser != null && _selectedUser is RegisteredUser selectedUser)
            {
                SelectedUser = selectedUser;
                _navigation.AnotherRegisteredUser = selectedUser;
                var allMessages = currentuser.GetChatMessages(selectedUser, currentuser);
                ChatMessagesForUI = allMessages.Select(m => new MessageViewModel(m, currentuser.Nickname)).ToList();
            }
        }
        public ICommand SendMessageCommand => new RelayCommand(obj =>
        {
            try
            {
                if (SelectedUser is RegisteredUser reguser)
                    currentuser.SendMessage(reguser, MessageContent);
                _system.SaveData();
                LoadMessages(SelectedUser);
                MessageContent = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
        public ICommand SendC => new RelayCommand(obj =>
        {
            var ComplaintWindow = new View.SendComplaint(_navigation, _system, null);
            ComplaintWindow.ShowDialog();
        });
        public ICommand Quit => new RelayCommand(obj =>
        {
            _navigation.RegisteredUserToPass = null;
            _navigation.ad = null;
            _navigation.AnotherRegisteredUser = null;
            HomeCommand.Execute(null);
        });


    }
}
