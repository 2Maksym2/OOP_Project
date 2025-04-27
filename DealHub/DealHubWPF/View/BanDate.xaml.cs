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

namespace DealHubWPF.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для BanDate.xaml
    /// </summary>
    public partial class BanDate : Window
    {
        public BanDate()
        {
            InitializeComponent();
            SelectedDate = DateTime.Now;
            DataContext = this;
        }
        public DateTime? SelectedDate { get; set; }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (BlockDatePicker.SelectedDate == null || BlockDatePicker.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("Оберіть правильну дату.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedDate = BlockDatePicker.SelectedDate;
            DialogResult = true;
            Close();
        }
    }
}
