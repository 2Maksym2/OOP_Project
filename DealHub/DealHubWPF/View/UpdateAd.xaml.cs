using DealHubWPF.ViewModel;
using Microsoft.Win32;
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
    /// Логика взаимодействия для UpdateAd.xaml
    /// </summary>
    public partial class UpdateAd : UserControl
    {
        public UpdateAd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UpdateAdVM Vm)
            {
                Vm.AdUpdateCommand.Execute(null);
            }

        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                if (DataContext is UpdateAdVM vm)
                {
                    vm.Image = filePath;
                }
            }
        }
    }
}
