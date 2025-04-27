using DealHub;
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

namespace DealHubWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AdminComplaints.xaml
    /// </summary>
    public partial class AdminComplaints : UserControl
    {
        public AdminComplaints()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Видалити скаргу?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                if (sender is Button btn && btn.DataContext is ComplaintsForView complaintDel)
                {
                    if (this.DataContext is AdminComplaintsVM Vm)
                    {
                        Vm.DeleteComplaintCommand.Execute(complaintDel);
                    }
                }

        }
    }
}
