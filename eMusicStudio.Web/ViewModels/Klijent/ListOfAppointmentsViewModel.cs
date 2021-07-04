using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class ListOfAppointmentsViewModel
    {
        public int TerminId { get; set; }
        public DateTime Datum { get; set; }
        [Display(Name ="Pocetak")]
        public TimeSpan VrijemeOd { get; set; }
        [Display(Name ="Kraj")]
        public TimeSpan VrijemeDo { get; set; }
    }
}
