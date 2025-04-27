using DealHubWPF.Model;
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
using DealHub;

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AdminUsersList.xaml
    /// </summary>
    public partial class AdminUsersList : UserControl
    {
        public AdminUsersList()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                    if (sender is Button btn && btn.DataContext is RegisteredUser user)
                    {
                        if (this.DataContext is AdminUsersListVM Vm)
                        {
                            Vm.BanUserCommand.Execute(user);
                        }
                    }         
        }

    }
}
