using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Uloge
    {
        public Uloge()
        {
            Korisnici = new HashSet<Korisnici>();
        }
        
        public int UlogaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public virtual ICollection<Korisnici> Korisnici { get; set; }

    }
}
