using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Klijenti
    {
        public Klijenti()
        {
            Ocjene = new HashSet<Ocjene>();
            Rezervacije = new HashSet<Rezervacije>();
            RezervacijeGluveSobe = new HashSet<RezervacijeGluveSobe>();
        }

        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public string Slika { get; set; }
        public int GradId { get; set; }
        public bool Banovan { get; set; }

        public virtual Grad Grad { get; set; }
        public virtual ICollection<Ocjene> Ocjene { get; set; }
        public virtual ICollection<Rezervacije> Rezervacije { get; set; }
        public virtual ICollection<RezervacijeGluveSobe> RezervacijeGluveSobe { get; set; }
    }
}
