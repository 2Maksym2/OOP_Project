using DealHubWPF.Model;
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
using DealHub;
using DealHubWPF.ViewModel;

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AdminAdsList.xaml
    /// </summary>
    public partial class AdminAdsList : UserControl
    {
        public AdminAdsList()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете видалити це оголошення?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                if (sender is Button btn && btn.DataContext is Ad adToDel)
                {
                    if (this.DataContext is AdminAdsListVM Vm)
                    {
                        Vm.DeleteAdCommand.Execute(adToDel);
                    }
                }

        }

    }
}
