using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model
{
    public class RezervacijeGluveSobe
    {
        public int RezervacijeGluveSobeId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan VrijemeOd { get; set; }
        public TimeSpan VrijemeDo { get; set; }
        public int KlijentId { get; set; }
        //public int TerminId { get; set; }
        //public bool Status { get; set; }
        public virtual Klijenti Klijent { get; set; }
        //public virtual Termini Termin { get; set; }
    }
}
