using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class ClientChangePasswordViewModel
    {
        [Required(ErrorMessage ="Trenutna lozinka je obavezno polje.")]
        [Display(Name ="Trenutna lozinka")]
        public string TrenutnaLozinka { get; set; }
        [Required(ErrorMessage ="Nova lozinka je obavezno polje.")]
        [Display(Name ="Nova lozinka")]
        public string NovaLozinka { get; set; }
        [Required(ErrorMessage ="Potvrda lozinke je obavezno polje.")]
        [Display(Name ="Potvrda lozinke")]
        [Compare("NovaLozinka",ErrorMessage ="Lozinke se ne poklapaju.")]
        public string PotvrdaLozinke { get; set; }
    }
}
