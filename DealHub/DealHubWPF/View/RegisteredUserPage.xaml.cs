using DealHub;
using DealHubWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для RegisteredUserPage.xaml
    /// </summary>
    public partial class RegisteredUserPage : UserControl
    {
        public ObservableCollection<Ad> AdsForUser { get; set; }
        public Button? _previousCategoryButton;
        public RegisteredUserPage()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var adItem = border?.DataContext;

            if (adItem != null && this.DataContext is RegisteredUserPageVM regpVM)
            {
                if (regpVM.GoAdPageCommand != null && regpVM.GoAdPageCommand.CanExecute(adItem))
                {
                    regpVM.GoAdPageCommand.Execute(adItem);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string? name = null;
            if (sender is Button clickedButton)
            {
                if (_previousCategoryButton != null)
                {
                    _previousCategoryButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6A5ACD"));
                }

                if (_previousCategoryButton == clickedButton)
                {
                    _previousCategoryButton = null;
                    name = null;
                }

                else
                {
                    clickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9370DB"));
                    _previousCategoryButton = clickedButton;
                    name = clickedButton.Name;
                }

                if (this.DataContext is RegisteredUserPageVM regUserVm)
                {
                    Category? category = null;

                    if (!string.IsNullOrWhiteSpace(name) && Enum.TryParse(typeof(Category), name, out var categoryObj) && categoryObj is Category parsed)
                    {
                        category = parsed;
                    }

                    if (regUserVm.SelectCategoryCommand != null && regUserVm.SelectCategoryCommand.CanExecute(category))
                    {
                        regUserVm.SelectCategoryCommand.Execute(category);
                    }
                }
            }
        }


    }
}
