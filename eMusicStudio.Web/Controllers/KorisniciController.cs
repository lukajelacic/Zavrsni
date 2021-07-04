using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using eMusicStudio.Model;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Helper;
using eMusicStudio.Web.Security;
using eMusicStudio.Web.Services;
using eMusicStudio.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eMusicStudio.Web.Controllers
{
    public class KorisniciController : Controller
    {
        private readonly IKlijentiService _klijentiService;
        private readonly IRezervacijeService _rezervacijeService;
        private readonly ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> _terminiService;
        private readonly ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> _muzickaOpremaService;
        private readonly IKorisniciService _korisniciService;
        private readonly IService<Model.Uloge, object> _ulogeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KorisniciController(IKorisniciService korisniciService, IService<Model.Uloge, object> ulogeService, IWebHostEnvironment webHostEnvironment,IRezervacijeService rezervacijeService,IKlijentiService klijentiService, ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> terminiService, ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> muzickaOpremaService)
        {
            _korisniciService = korisniciService;
            _ulogeService = ulogeService;
            _webHostEnvironment = webHostEnvironment;
            _klijentiService = klijentiService;
            _muzickaOpremaService = muzickaOpremaService;
            _rezervacijeService = rezervacijeService;
            _terminiService = terminiService;
        }
        public IActionResult Index()
        {
            var rezervacije = _rezervacijeService.Get(null);
            decimal zarada = 0;
            foreach (var item in rezervacije)
            {
                zarada += item.Cijena.Value;
            }
            var model = new IndexVievModel()
            {
                BrojKlijenata = _klijentiService.Get(null).Count(),
                BrojTermina=_terminiService.Get(null).Count(),
                BrojRezervacija=_rezervacijeService.Get(null).Count(),
                BrojInstrumenata=_muzickaOpremaService.Get(null).Count(),
                UkupnaZarada=zarada,
            };
           
            return View(model);
        }
        [HttpGet]

        public IActionResult KreirajKorisnika()
        {
            var model = new CreateUserViewModel();
            var listaUloga = _ulogeService.Get(null);
            var list = new List<SelectListItem>();
            foreach (var item in listaUloga)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.UlogaId.ToString(),
                    Text = item.Naziv
                });
            }
            model.Uloge = list;
            return View(model);
        }
        [HttpPost]
        public IActionResult KreirajKorisnika(CreateUserViewModel model)
        {
            var user = new KorisniciInsertRequest();
            
            if (ModelState.IsValid)
            {
                user.Ime = model.Ime;
                user.Prezime = model.Prezime;
                user.KorisnickoIme = model.KorisnickoIme;
                user.Email = model.Email;
                user.Telefon = model.Telefon;
                user.UlogaId = model.UlogaId;
                user.Password = model.Password;
                user.PasswordConfirmation = model.PasswordConfirmation;
                _korisniciService.Insert(user);
                TempData["korisnik"] = "Uspjesno dodan novi radnik!";
                return RedirectToAction(nameof(ListaKorisnika));
            }
            var lista = _korisniciService.Get(null);
            var viewModel = new CreateUserViewModel();
            var listaUloga = _ulogeService.Get(null);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in lista)
            {
                if (item.KorisnickoIme == model.KorisnickoIme)
                {
                    ModelState.AddModelError("KorisnickoIme", "Korisnicko ime je zauzeto.");
                    //viewModel = new CreateUserViewModel();
                   // listaUloga = _ulogeService.Get(null);
                    list = new List<SelectListItem>();
                    foreach (var item1 in listaUloga)
                    {
                        list.Add(new SelectListItem()
                        {
                            Value = item1.UlogaId.ToString(),
                            Text = item1.Naziv
                        });
                    }
                    viewModel.Uloge = list;
                    return View(viewModel);
                }
            }

            foreach (var item in listaUloga)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.UlogaId.ToString(),
                    Text = item.Naziv
                });
            }
            viewModel.Uloge = list;
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ListaKorisnika()
        {
            var list = _korisniciService.Get(null);
            var model = new List<UserListViewModel>();
            foreach (var item in list)
            {
                model.Add(new UserListViewModel()
                {
                    KorisnikId = item.KorisnikId,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    KorisnickoIme = item.KorisnickoIme
                });
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult ListaKorisnika(string korisnickoIme)
        {
            var list = _korisniciService.Get(new KorisniciSearchRequest()
            {
                KorisnickoIme = korisnickoIme
            });
            var model = new List<UserListViewModel>();
            foreach (var item in list)
            {
                model.Add(new UserListViewModel()
                {
                    KorisnikId = item.KorisnikId,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    KorisnickoIme = item.KorisnickoIme
                });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult DetaljiKorisnika(int id)
        {
            var user = _korisniciService.GetById(id);
            Model.Uloge uloga = _ulogeService.GetById(user.UlogaId);
            var model = new UserDetailViewModel()
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Telefon = user.Telefon,
                Email = user.Email,
                Uloga = uloga.Naziv
            };
            return View(model);

        }

        public IActionResult Logout()
        {
            GlobalUser.prijavljeniKorisnik = null;
            GlobalUser.prijavljeniKorisnikId = -1;
            return RedirectToAction("Login", "Login");
        }

    }
}
