using System;
using System.Collections.Generic;

namespace eMusicStudio.Web.Models
{
    public partial class MuzickaOprema
    {
        public MuzickaOprema()
        {
            Ocjene = new HashSet<Ocjene>();
            RezervacijaStavke = new HashSet<RezervacijaStavke>();
        }

        public int MuzickaOpremaId { get; set; }
        public string Naziv { get; set; }
        public int VrstaId { get; set; }
        public int NaStanju { get; set; }
        public string Slika { get; set; }
        public int Cijena { get; set; }
        public virtual Vrsta Vrsta { get; set; }
        public virtual ICollection<Ocjene> Ocjene { get; set; }
        public virtual ICollection<RezervacijaStavke> RezervacijaStavke { get; set; }
    }
}
