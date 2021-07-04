using eMusicStudio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class CreateClientViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno polje.")]
        [MinLength(4, ErrorMessage = "Ime mora imati minimalno 4 znaka.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno polje.")]
        [MinLength(4, ErrorMessage = "Prezime mora imati minimalno 4 znaka.")]
        public string Prezime { get; set; }

        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Unesite email u pravilnom formatu, kao na primjer test@test.com.")]
        [Required(ErrorMessage = "Email je obavezno polje.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezno polje.")]
        [RegularExpression(@"(06[0-9])([0-9]){3}([0-9]){3}$", ErrorMessage = "Telefonski broj mora biti u sledecem formatu: 06XXXXXXX.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Korisnicko ime je obavezno polje.")]
        [MinLength(4, ErrorMessage = "Korisnicko ime mora imati minimalno 4 znaka.")]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }

        public List<SelectListItem> Gradovi { get; set; }
        [Required(ErrorMessage = "Grad je obavezno polje.")]
        public int GradId { get; set; }

        [Required(ErrorMessage ="Slika je obavezno polje.")]
        public IFormFile Slika { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Molim vas potvrdite lozinku.")]
        [Display(Name = "Potvrda lozinke")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne slazu.")]
        public string PasswordConfirmation { get; set; }

        public bool Banovan { get; set; }
    }
}
