using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMusicStudio.Web.Services;
using eMusicStudio.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMusicStudio.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IKorisniciService _korisniciService;
        private readonly IKlijentiService _klijentiService;
        public LoginController(IKorisniciService korisniciService,IKlijentiService klijentiService)
        {
            _korisniciService = korisniciService;
            _klijentiService = klijentiService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.IsAdmin)
            {
                var login = _korisniciService.Authenticiraj(model.Username, model.Password);
                if (login != null)
                {
                    GlobalUser.prijavljeniKorisnik = login;
                    GlobalUser.prijavljeniKorisnikId = login.KorisnikId;
                    return RedirectToAction("Index", "Korisnici");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                var login = _klijentiService.Authenticiraj(model.Username, model.Password);
                if (login != null)
                {
                    GlobalClient.prijavljeniKlijent = login;
                    GlobalClient.prijavljeniKlijentId = login.KlijentId;
                    return RedirectToAction("Index", "Klijenti");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}
