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

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AdPage.xaml
    /// </summary>
    public partial class AdPage : UserControl
    {
        public AdPage()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AdPageVM Vm)
            {
                Vm.SendMessageCommand.Execute(null);
                MyScrollViewer.ScrollToEnd();
            }

        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.DataContext is AdPageVM Vm)
                {
                    Vm.SendMessageCommand.Execute(null);
                    MyScrollViewer.ScrollToEnd();
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)        
        {
              if (ChatBox.Visibility == Visibility.Collapsed)
              {
                    ChatBox.Visibility = Visibility.Visible;
                    if (this.DataContext is AdPageVM Vm)
                    {
                        Vm.LoadMessages();
                        MyScrollViewer.ScrollToEnd();
                    }
              }
              else ChatBox.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AdPageVM Vm)
            {
                Vm.SendR.Execute(null);
            }
        }

        private void UserButton1_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AdPageVM Vm)
            {
                Vm.SendC.Execute(null);
            }

        }
    }
}
