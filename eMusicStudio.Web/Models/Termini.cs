using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Termini
    {
        //public Termini()
        //{
        //    RezervacijeGluveSobe = new HashSet<RezervacijeGluveSobe>();
        //}

        public int TerminId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan VrijemeOd { get; set; }
        public TimeSpan VrijemeDo { get; set; }
        public int KorisnikId { get; set; }
        public bool? Aktivan { get; set; }
        //public bool Status { get; set; }

        public virtual Korisnici Korisnik { get; set; }
        //public virtual ICollection<RezervacijeGluveSobe> RezervacijeGluveSobe { get; set; }
    }
}
