using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Korisnicko ime je obavezno polje.")]
        [Display(Name ="Korisnicko ime")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezno polje.")]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
