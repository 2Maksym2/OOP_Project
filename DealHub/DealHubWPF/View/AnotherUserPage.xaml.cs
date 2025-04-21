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

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AnotherUserPage.xaml
    /// </summary>
    public partial class AnotherUserPage : UserControl
    {
        public AnotherUserPage()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var adItem = border?.DataContext;

            // Отримуємо DataContext сторінки, який є HomeVM
            if (adItem != null && this.DataContext is AnotherUserVM userVM)
            {
                if (userVM.GoAdPageCommand != null && userVM.GoAdPageCommand.CanExecute(adItem))
                {
                    userVM.GoAdPageCommand.Execute(adItem);
                }
            }
        }

    }
}
