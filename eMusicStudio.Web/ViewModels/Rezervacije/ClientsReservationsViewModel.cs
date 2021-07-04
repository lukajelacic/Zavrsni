using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Rezervacije
{
    public class ClientsReservationsViewModel
    {
        public int RezervacijaId { get; set; }
        public int BrojRezervacije { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public decimal? Cijena { get; set; }

    }
}
