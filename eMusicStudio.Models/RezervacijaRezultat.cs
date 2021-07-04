using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    
    public class RezervacijaRezultat
    {
        public int RezervacijaId { get; set; }
        public int BrojRezervacije { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public bool? Otkazano { get; set; }
        public string Klijent { get; set; }
        public int? KorisnikId { get; set; }
        public bool Status { get; set; }
        public decimal? Cijena { get; set; }

        public RezervacijaRezultat(Rezervacija item)
        {
            RezervacijaId = item.RezervacijaId;
            BrojRezervacije = item.BrojRezervacije;
            Datum = item.Datum;
            Otkazano = item.Otkazano;
            Klijent = item.Klijent.Ime + " " + item.Klijent.Prezime;
            KorisnikId = item.KorisnikId;
            Status = item.Status;
            DatumRezervacije = item.DatumRezervacije;
            Cijena = item.Cijena;
        }
    }
}
