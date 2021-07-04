using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class RezervacijaStavke
    {
        public int RezervacijaStavkaId { get; set; }
        public int RezervacijaId { get; set; }
        public int MuzickaOpremaId { get; set; }
        public int Kolicina { get; set; }

        public virtual MuzickaOprema MuzickaOprema { get; set; }
        public virtual Rezervacije Rezervacija { get; set; }
    }
}
