using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eMusicStudio.Model
{
    public class MuzickaOprema
    {
        public int MuzickaOpremaId { get; set; }
        public string Naziv { get; set; }
        public int VrstaId { get; set; }
        [Display(Name ="Na stanju")]
        public int NaStanju { get; set; }
        public string Slika { get; set; }
        public int Cijena { get; set; }
    }
}
