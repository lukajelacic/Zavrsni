using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class TerminiInsertRequest
    {
        public DateTime Datum { get; set; }
        public TimeSpan VrijemeOd { get; set; }
        public TimeSpan VrijemeDo { get; set; }
        public int KorisnikId { get; set; }
        public bool? Aktivan { get; set; }
    }
}
