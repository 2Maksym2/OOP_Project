using DealHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHubWPF.Model
{
    class ComplaintsForView
    {
        public Complaint Complaint { get; }
        public string DisplayTitle { get; set; }

        public ComplaintsForView(Complaint complaint)
        {
            Complaint = complaint;
            if(Complaint.AdTitle != null)
                DisplayTitle = Complaint.AdTitle;
            else DisplayTitle = Complaint.ReceiverName;
        }

        // Для зручності доступу
        public string Content => Complaint.Description;
        public int Id => Complaint.Id;

    }
}
