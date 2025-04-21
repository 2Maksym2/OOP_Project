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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Будь ласка, заповніть всі поля!", "Помилка", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                if (sender is Button btn && btn.DataContext is Ad adToRemove)
                {
                    if (this.DataContext is ProfileVM profileVm)
                    {
                        profileVm.DeleteAdCommand.Execute(adToRemove);
                    }
                }

        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var adItem = border?.DataContext;

            if (adItem != null && this.DataContext is ProfileVM profileVm)
            {
                if (profileVm.AdPageCommand != null && profileVm.AdPageCommand.CanExecute(adItem))
                {
                    profileVm.AdPageCommand.Execute(adItem);
                }
            }
        }

    }
}
