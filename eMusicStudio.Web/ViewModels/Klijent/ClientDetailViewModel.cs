using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.Klijent
{
    public class ClientDetailViewModel
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public string Grad { get; set; }
        public string Slika { get; set; }
        public string Banovan { get; set; }
        public bool Ban { get; set; }
        public int BrojRezervacija { get; set; }
        public int BrojTermina { get; set; }
    }
}
