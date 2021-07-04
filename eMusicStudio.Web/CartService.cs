using eMusicStudio.Web.ViewModels.Rezervacije;
using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Web
{
    public static class CartService
    {
        public static Dictionary<int, RezervacijaViewModel> Cart { get; set; } = new Dictionary<int, RezervacijaViewModel>();
    }
}
