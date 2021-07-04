using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Admin
{
    public class IndexVievModel
    {
        public int BrojKlijenata { get; set; }
        public int BrojRezervacija { get; set; }
        public int BrojTermina { get; set; }
        public int BrojInstrumenata { get; set; }
        public decimal UkupnaZarada { get; set; }
    }
}
