using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class Grad
    {
        public Grad()
        {
            Klijenti = new HashSet<Klijenti>();
        }

        public int GradId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Klijenti> Klijenti { get; set; }
    }
}
