using eMusicStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Rezervacije
{
    public class RezervacijaViewModel
    {
        public Model.MuzickaOprema MuzickaOprema { get; set; }
        public int Kolicina { get; set; }
    }
}
