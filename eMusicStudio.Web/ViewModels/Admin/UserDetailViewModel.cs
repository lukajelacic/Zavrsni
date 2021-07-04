using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Admin
{
    public class UserDetailViewModel
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Uloga { get; set; }
    }
}
