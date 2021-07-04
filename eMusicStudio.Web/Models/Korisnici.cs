using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            Rezervacije = new HashSet<Rezervacije>();
            Termini = new HashSet<Termini>();
        }

        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public int UlogaId { get; set; }
        public virtual Uloge Uloga { get; set; }

        public virtual ICollection<Rezervacije> Rezervacije { get; set; }
        public virtual ICollection<Termini> Termini { get; set; }
    }
}
