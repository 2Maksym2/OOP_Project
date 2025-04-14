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
    /// Логика взаимодействия для Chats.xaml
    /// </summary>
    public partial class Chats : UserControl
    {
        private Button _previousbutton;
        public Chats()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool? ispresed = null;
            if (sender is Button clickedButton)
            {
                if (_previousbutton != null)
                {
                    _previousbutton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
                }
                
                    clickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
                    _previousbutton = clickedButton;
                

                if (this.DataContext is ChatsVM chatVm)
                {

                    if (clickedButton.Name == "Buyer")
                    {
                        chatVm.IsBuyerSelected=true;
                    }
                    else
                    {
                        chatVm.IsSellerSelected = true;
                    }
                }
            }

        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var obj = border?.DataContext;
            if(obj is User UserItem)
            if (UserItem != null && this.DataContext is ChatsVM chatVm)
            {
                chatVm.LoadMessages(UserItem);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ChatsVM chatVm && chatVm.SelectedUser != null)
            {
                    chatVm.SendMessageCommand.Execute(null);
            }

        }
    }
}
