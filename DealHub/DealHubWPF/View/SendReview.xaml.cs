using DealHub;
using DealHubWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Review.xaml
    /// </summary>
    public partial class SendReview : Window
    {
        public string FeedbackText { get; private set; }
        private readonly NavigationVM _navigation;
        private readonly DealHubSystem _system;

        internal SendReview(NavigationVM navigation, DealHubSystem system)
        {
            InitializeComponent();
            _navigation = navigation;
            _system = system;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            FeedbackText = FeedbackTextBox.Text;
            try
            {
                _navigation.RegisteredUserToPass.LeaveReview(_navigation.AnotherRegisteredUser, FeedbackText);
                _system.SaveData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
