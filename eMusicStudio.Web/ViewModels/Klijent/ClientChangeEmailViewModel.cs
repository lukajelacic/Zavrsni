using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class ClientChangeEmailViewModel
    {
        [Display(Name ="Trenutni email")]
        public string TrenutniEmail { get; set; }
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Unesite email u pravilnom formatu, kao na primjer test@test.com.")]
        [Display(Name ="Novi email")]
        [Required(ErrorMessage = "Email je obavezno polje.")]
        public string NoviEmail { get; set; }
    }
}
