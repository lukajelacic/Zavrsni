using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model
{
    public class Ocjene
    {
        public int OcjenaId { get; set; }
        public DateTime Datum { get; set; }
        public int Ocjena { get; set; }
        public int KlijentId { get; set; }
        public int MuzickaOpremaId { get; set; }
    }
}
