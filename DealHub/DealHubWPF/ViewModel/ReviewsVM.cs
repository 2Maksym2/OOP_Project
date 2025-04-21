using DealHub;
using DealHubWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class ReviewsVM:Utilities.ViewModelBase
    {
        public ICommand HomeCommand { get; }
        public ICommand RegUserCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand ChatsCommand { get; }
        private RegisteredUser _currentuser;
        private List<Review> _reviewList;
        public List<Review> ReviewList
        {
            get => _reviewList;
            set
            {
                _reviewList = value;
                OnPropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            private set => _userName = value;
        }

        public ReviewsVM(NavigationVM navigation, RegisteredUser currentUser)
        {
            _currentuser = currentUser;
            UserName = _currentuser.Nickname;
            HomeCommand = navigation.HomeCommand;
            RegUserCommand = navigation.RegisteredUserPCommand;
            ProfileCommand = navigation.ProfileCommand;
            ChatsCommand = navigation.ChatsCommand;
            ReviewList = _currentuser.Reviews;
        }
    }
}
