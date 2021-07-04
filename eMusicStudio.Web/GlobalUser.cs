using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web
{
    public  class GlobalUser
    {
        public static Model.Korisnici prijavljeniKorisnik { get; set; }
        public static int prijavljeniKorisnikId { get; set; }
    }
}
