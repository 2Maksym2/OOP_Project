using DealHubWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHubWPF.Model;
using DealHub;
using DealHubWPF.Utilities;
using System.Windows.Input;

namespace DealHubWPF.ViewModel
{
    class AdminComplaintsVM:Utilities.ViewModelBase
    {
        private Admin Admin;
        private List<ComplaintsForView> _complaints;
        public List<ComplaintsForView> Complaints
        {
            get => _complaints;
            set
            {
                _complaints = value;
                OnPropertyChanged();
            }
        }
        private DealHubSystem _system;

        public ICommand AdminPanelCommand { get; }
        public AdminComplaintsVM(NavigationVM navigation, DealHubSystem system) 
        {
            _system = system;
            Admin = navigation.Admin;
            AdminPanelCommand = navigation.AdminPanelCommand;
            Complaints = system.Complaints.Select(m => new ComplaintsForView(m)).ToList();
        }
        public ICommand DeleteComplaintCommand => new RelayCommand(obj =>
        {
            if(obj is ComplaintsForView complaint)
            Admin.DeleteComplaint(_system, complaint.Id);
            Complaints = _system.Complaints.Select(m => new ComplaintsForView(m)).ToList();
            _system.SaveData();
        });

    }
}
