using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Termini
{
    public class ListOfAllAppointmentsViewModel
    {
        public List<CreateAppointmentViewModel> Lista { get; set; }
        public bool IsAdmin { get; set; }
    }
}
