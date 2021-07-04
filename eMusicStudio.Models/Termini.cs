using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model
{
    public class Termini
    {
        public int TerminId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan VrijemeOd { get; set; }
        public TimeSpan VrijemeDo { get; set; }
        public int KorisnikId { get; set; }
        public bool? Aktivan { get; set; }
        // public bool Status { get; set; }
    }
}
