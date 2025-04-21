using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DealHub;

namespace DealHubWPF.ViewModel
{
    class AnotherUserReviewsVM:Utilities.ViewModelBase
    {
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        public ICommand AnotherUserCommand { get; }
        public RegisteredUser SelectedUser { get; set; }
        private string _userName;
        public string UserName
        {
            get => _userName;
            private set => _userName = value;
        }
        private List<Review> _reviews;
        public List<Review> Reviews
        {
            get => _reviews;
            set
            {
                _reviews = value;
                OnPropertyChanged();
            }
        }


        public AnotherUserReviewsVM(NavigationVM navigation, RegisteredUser selectedUser)
        {
            SelectedUser = selectedUser;
            UserName = SelectedUser.Nickname;
            AnotherUserCommand = navigation.AnotherUserCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
            Reviews = SelectedUser.Reviews; 
        }

    }
}
