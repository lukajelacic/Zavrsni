using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class OcjenaInsertRequest
    {
        public DateTime Datum { get; set; }
        public int Ocjena { get; set; }
        public int KlijentId { get; set; }
        public int MuzickaOpremaId { get; set; }
    }
}
