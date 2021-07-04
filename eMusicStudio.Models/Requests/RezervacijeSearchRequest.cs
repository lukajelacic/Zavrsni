using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class RezervacijeSearchRequest
    {
        public bool? Status { get; set; }
        public int? KlijentId { get; set; }
        public DateTime? DatumRezervacije { get; set; }
    }
}
