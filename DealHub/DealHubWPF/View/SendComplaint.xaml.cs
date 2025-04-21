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
    /// Логика взаимодействия для SendComplaint.xaml
    /// </summary>
    public partial class SendComplaint : Window
    {
        public string ComplaintText { get; private set; }
        private readonly NavigationVM _navigation;
        private readonly DealHubSystem _system;
        private readonly Ad? _ad;

        internal SendComplaint(NavigationVM navigation, DealHubSystem system, Ad? ad)
        {
            InitializeComponent();
            _navigation = navigation;
            _system = system;
            _ad = ad;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            ComplaintText = ComplaintTextBox.Text;
            try
            {
                _navigation.RegisteredUserToPass.SendComplaint(_system, _navigation.AnotherRegisteredUser, _ad, ComplaintText);
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
