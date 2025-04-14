using System.Text;
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
namespace DealHubWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var system = new DealHubSystem();
            DataContext = new NavigationVM(system);
            system.LoadData();            
            var navigationVM = new NavigationVM(system);
        }
    }
}