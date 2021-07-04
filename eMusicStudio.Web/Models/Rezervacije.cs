using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Rezervacije
    {
        public Rezervacije()
        {
            RezervacijaStavke = new HashSet<RezervacijaStavke>();
        }

        public int RezervacijaId { get; set; }
        public int BrojRezervacije { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public bool? Otkazano { get; set; }
        public int KlijentId { get; set; }
        public int? KorisnikId { get; set; }
        public bool Status { get; set; }
        public decimal? Cijena { get; set; }
        public bool? Arhivirana { get; set; }

        public virtual Klijenti Klijent { get; set; }
        public virtual Korisnici Korisnik { get; set; }
        public virtual ICollection<RezervacijaStavke> RezervacijaStavke { get; set; }
    }
}
