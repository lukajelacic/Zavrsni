using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.MuzickaOprema
{
    public class NumberInStockViewModel
    {
        public int MuzickaOpremaId { get; set; }
        public string Naziv { get; set; }
        public int NaStanju { get; set; }
        [Required(ErrorMessage ="Kolicina je obavezno polje.")]
        [Range(1,10,ErrorMessage ="Kolicina mora imati vrijednost izmedju 1 i 10.")]
        public int Kolicina { get; set; }
    }
}
