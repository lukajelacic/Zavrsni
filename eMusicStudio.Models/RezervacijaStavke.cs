using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model
{
    public class RezervacijaStavke
    {
        public int RezervacijaStavkaId { get; set; }
        public int RezervacijaId { get; set; }
        public int MuzickaOpremaId { get; set; }
        public int Kolicina { get; set; }

        public virtual MuzickaOprema MuzickaOprema { get; set; }
        public virtual Rezervacija Rezervacija { get; set; }
    }
}
