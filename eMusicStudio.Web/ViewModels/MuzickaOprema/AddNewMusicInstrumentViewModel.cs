using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.MuzickaOprema
{
    public class AddNewMusicInstrumentViewModel
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="Naziv je obavezno polje.")]
        public string Naziv { get; set; }
        public List<SelectListItem> Vrste { get; set; }
        [Required(ErrorMessage ="Vrsta muzickog instrumenta je obavezno polje.")]
        public int VrstaId { get; set; }
       
        [Range(1, 50,ErrorMessage ="Broj instrumenata na stanju mora biti izmedju 1 i 50 komada.")]
        [Required(ErrorMessage ="Na stanju je obavezno polje.")]
        [Display(Name ="Na stanju")]
        public int NaStanju { get; set; }
        [Required(ErrorMessage ="Slika muzickog instrumenta je obavezna.")]
        public IFormFile Slika { get; set; }
        [Required(ErrorMessage ="Cijena je obavezno polje.")]
        [Range(1, 500,ErrorMessage ="Cijena mora imati vrijednost izmedju 1 i 500KM.")]
        public int Cijena { get; set; }
    }
}
