using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Vrsta
    {
        public Vrsta()
        {
            MuzickaOprema = new HashSet<MuzickaOprema>();
        }

        public int VrstaId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<MuzickaOprema> MuzickaOprema { get; set; }
    }
}
