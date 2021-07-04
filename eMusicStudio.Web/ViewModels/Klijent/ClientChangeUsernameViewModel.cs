using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class ClientChangeUsernameViewModel
    {
        [Display(Name ="Korisnicko ime")]
        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Korisnicko ime mora imati minimalno 4 karaktera.")]
        [MinLength(4,ErrorMessage ="Korisnicko ime mora imati minimalno 4 karaktera.")]
        [Display(Name ="Novo korisnicko ime")]
        public string NovoKorisnickoIme { get; set; }
    }
}
