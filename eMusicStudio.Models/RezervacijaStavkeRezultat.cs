using System;
using System.Collections.Generic;
using System.Text;


namespace eMusicStudio.Model
{
    public class RezervacijaStavkeRezultat
    {
        public int RezervacijaStavkaId { get; set; }
        public int RezervacijaId { get; set; }
        public string MuzickaOprema { get; set; }
        public int Kolicina { get; set; }
        public decimal Cijena { get; set; }
        public RezervacijaStavkeRezultat(RezervacijaStavke item)
        {
            RezervacijaStavkaId = item.RezervacijaStavkaId;
            RezervacijaId = item.RezervacijaId;
            MuzickaOprema = item.MuzickaOprema.Naziv;
            Kolicina = item.Kolicina;
            Cijena = item.MuzickaOprema.Cijena;
        }
    }
}
