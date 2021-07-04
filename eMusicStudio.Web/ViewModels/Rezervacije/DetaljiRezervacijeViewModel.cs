using eMusicStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Rezervacije
{
    public class DetaljiRezervacijeViewModel
    {
        public int RezervacijaId { get; set; }
        public int BrojRezervacije { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public decimal Cijena { get; set; }
        public List<RezervacijaStavkeRezultat> RezervacijeStavke { get; set; }
        public bool Otkazana { get; set; }
    }
}
