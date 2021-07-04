using eMusicStudio.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Termini
{
    public class CreateAppointmentViewModel
    {
        [Required(ErrorMessage ="Datum je obavezno polje.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Datum { get; set; }
        [Required(ErrorMessage ="Pocetak je obavezno polje.")]
        [Display(Name ="Početak:")]
        public TimeSpan? VrijemeOd { get; set; }
        [Required(ErrorMessage ="Kraj je obavezno polje.")]
        [Display(Name ="Kraj:")]
        public TimeSpan? VrijemeDo { get; set; }
        public int KorisnikId { get; set; }
    }
}
