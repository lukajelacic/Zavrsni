using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class KlijentiInsertRequest
    {
        [Required]
        [MinLength(4)]
        public string Ime { get; set; }
        [Required]
        [MinLength(4)]
        public string Prezime { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(9)]
        public string Telefon { get; set; }
        [Required]
        [MinLength(4)]
        public string KorisnickoIme { get; set; }
        public int GradId { get; set; }
        public string Slika { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public bool Banovan { get; set; }
    }
}
