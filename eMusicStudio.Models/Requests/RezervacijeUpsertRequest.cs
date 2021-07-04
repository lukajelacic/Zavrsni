using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class RezervacijeUpsertRequest
    {
        public int BrojRezervacije { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public bool? Otkazano { get; set; }
        public int KlijentId { get; set; }
        public int? KorisnikId { get; set; }
        public bool Status { get; set; }
        public decimal? Cijena { get; set; }
        public bool? Arhivirana { get; set; }
    }
}
