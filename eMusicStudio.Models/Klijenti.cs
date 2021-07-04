using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model
{
    public class Klijenti
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public int GradId { get; set; }
        public string Grad { get; set; }
        public string Slika { get; set; }
        public bool Banovan { get; set; }
        //public string Password { get; set; }
        //public string PasswordConfirmation { get; set; }

    }
}
