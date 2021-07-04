using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class ClientAppointmentViewModel
    {
        public int TerminId { get; set; }
        public string Datum { get; set; }
        [Display(Name ="Pocetak")]
        public string VrijemeOd { get; set; }
        [Display(Name ="Kraj")]
        public string VrijemeDo { get; set; }
    }
}
