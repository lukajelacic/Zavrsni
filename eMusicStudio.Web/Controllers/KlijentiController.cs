using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eMusicStudio.Model;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Security;
using eMusicStudio.Web.Services;
using eMusicStudio.Web.ViewModels;
using eMusicStudio.Web.ViewModels.Admin;
using eMusicStudio.Web.ViewModels.Klijent;
using eMusicStudio.Web.ViewModels.Rezervacije;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eMusicStudio.Web.Controllers
{
    public class KlijentiController : Controller
    {
        private readonly IService<Model.Grad, object> _gradoviService;
        private readonly IRezervacijeService _rezervacijeService;
        private readonly IKlijentiService _klijentiService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> _terminiService;
        private readonly ICRUDService<Model.RezervacijeGluveSobe, RezervacijaGluveSobeSearchRequest, Model.RezervacijeGluveSobe, Model.RezervacijeGluveSobe> _rezervacijeGluveSobe;
        public KlijentiController( IService<Model.Grad, object> gradoviService, IRezervacijeService rezervacijeService,IKlijentiService klijentiService,IWebHostEnvironment webHostEnvironment, ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> terminiService, ICRUDService<Model.RezervacijeGluveSobe, RezervacijaGluveSobeSearchRequest, Model.RezervacijeGluveSobe, Model.RezervacijeGluveSobe> rezervacijeGluveSobe)
        {
            _gradoviService = gradoviService;
            _rezervacijeService = rezervacijeService;
            _klijentiService = klijentiService;
            _webHostEnvironment = webHostEnvironment;
            _terminiService = terminiService;
            _rezervacijeGluveSobe = rezervacijeGluveSobe;
        }
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult KreirajKlijenta()
        {
            var model = new ViewModels.Klijent.CreateClientViewModel();
            var listaGradova = _gradoviService.Get(null);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in listaGradova)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.GradId.ToString(),
                    Text = item.Naziv
                });
            }
            model.Gradovi = list;
            return View(model);
        }
        [HttpPost]
        public IActionResult KreirajKlijenta(ViewModels.Klijent.CreateClientViewModel model)
        {
            //srediti dodavanje sllike
            var klijent = new KlijentiInsertRequest();
            string uniqueFileName = Helper.Image.Upload(model.Slika, _webHostEnvironment);
            if (ModelState.IsValid)
            {
                klijent.Ime = model.Ime;
                klijent.Prezime = model.Prezime;
                klijent.KorisnickoIme = model.KorisnickoIme;
                klijent.GradId = model.GradId;
                klijent.Password = model.Password;
                klijent.PasswordConfirmation = model.PasswordConfirmation;
                klijent.Email = model.Email;
                klijent.Telefon = model.Telefon;
                klijent.Slika = uniqueFileName;
                _klijentiService.Insert(klijent);
                TempData["porukaDodavanje"] = "Uspjesno dodan novi klijent!";
                return RedirectToAction(nameof(ListaKlijenata));
            }
            var klijenti = _klijentiService.Get(null);
            var viewModel = new ViewModels.Klijent.CreateClientViewModel();
            var listaGradova = _gradoviService.Get(null);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in klijenti)
            {
                if (item.KorisnickoIme == model.KorisnickoIme)
                {
                    ModelState.AddModelError("KorisnickoIme", "Korisnicko ime je zauzeto.");
                    foreach (var item1 in listaGradova)
                    {
                        list.Add(new SelectListItem()
                        {
                            Value = item1.GradId.ToString(),
                            Text = item1.Naziv
                        });
                    }
                    viewModel.Gradovi = list;
                    return View(viewModel);
                }
            }
            
            foreach (var item in listaGradova)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.GradId.ToString(),
                    Text = item.Naziv
                });
            }
            viewModel.Gradovi = list;
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ListaKlijenata()
        {
            var list = _klijentiService.Get(null);
            var model = new List<ViewModels.Klijent.ClientListViewModel>();
            foreach (var item in list)
            {
                model.Add(new ViewModels.Klijent.ClientListViewModel()
                {
                    KlijentId = item.KlijentId,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    KorisnickoIme = item.KorisnickoIme
                });
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult ListaKlijenata(string korisnickoIme)
        {
            var list = _klijentiService.Get(new KlijentiSearchRequest()
            {
                KorisnickoIme = korisnickoIme
            });
            var model = new List<ViewModels.Klijent.ClientListViewModel>();
            foreach (var item in list)
            {
                model.Add(new ViewModels.Klijent.ClientListViewModel()
                {
                    KlijentId = item.KlijentId,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    KorisnickoIme = item.KorisnickoIme
                });
            }
            return View(model);
        }
        public IActionResult DetaljiKlijenta(int id)
        {
            var user = _klijentiService.GetById(id);
            var rezervacije = _rezervacijeService.Get(new RezervacijeSearchRequest { KlijentId = id });
            var termini = _rezervacijeGluveSobe.Get(new RezervacijaGluveSobeSearchRequest { KlijentId = id });
            var brojRezervacija = rezervacije.Count();
            var brojTermina = termini.Count();
            Model.Grad grad = _gradoviService.GetById(user.GradId);
            var model = new ViewModels.Klijent.ClientDetailViewModel()
            {
                KlijentId = user.KlijentId,
                Ime = user.Ime,
                Prezime = user.Prezime,
                KorisnickoIme = user.KorisnickoIme,
                Email = user.Email,
                Telefon = user.Telefon,
                Slika = user.Slika,
                Grad = grad.Naziv,
                Ban = user.Banovan,
                BrojRezervacija=brojRezervacija,
                BrojTermina=brojTermina
            };
            if (user.Banovan)
                model.Banovan = "DA";
            else
                model.Banovan = "NE";
            return View(model);
        }
        [HttpGet]
        public IActionResult ProfilKlijenta()
        {
            var user = _klijentiService.GetById(GlobalClient.prijavljeniKlijentId);
            Model.Grad grad = _gradoviService.GetById(user.GradId);
            var model = new ViewModels.Klijent.ClientDetailViewModel()
            {
                KlijentId = user.KlijentId,
                Ime = user.Ime,
                Prezime = user.Prezime,
                KorisnickoIme = user.KorisnickoIme,
                Email = user.Email,
                Telefon = user.Telefon,
                Slika = user.Slika,
                Grad = grad.Naziv,
                Ban = user.Banovan
            };
            if (user.Banovan)
                model.Banovan = "DA";
            else
                model.Banovan = "NE";
            return View(model);
        }
        [HttpGet]
        public IActionResult PromjeniKorisnickoIme()
        {
            var user = _klijentiService.GetById(GlobalClient.prijavljeniKlijentId);
            var model = new ClientChangeUsernameViewModel()
            {
                KorisnickoIme = user.KorisnickoIme
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PromjeniKorisnickoIme(ClientChangeUsernameViewModel model)
        {
            var klijent = _klijentiService.GetKlijenta(GlobalClient.prijavljeniKlijentId);
            if (ModelState.IsValid)
            {
                klijent.KorisnickoIme = model.NovoKorisnickoIme;
                _klijentiService.UpdateBanovan(klijent.KlijentId, klijent);
                TempData["korisnickoIme"] = "Korisnicko ime uspjesno promjenjeno!";
                return RedirectToAction(nameof(ProfilKlijenta));
            }
            var viewModel = new ClientChangeUsernameViewModel()
            {
                KorisnickoIme = klijent.KorisnickoIme
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult PromjeniLozinku()
        {
            var model = new ClientChangePasswordViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult PromjeniLozinku(ClientChangePasswordViewModel model)
        {
            var viewModel = new ClientChangePasswordViewModel();
            if (!ModelState.IsValid)
            {
                viewModel = new ClientChangePasswordViewModel();
                return View(viewModel);
            }
            var klijent = _klijentiService.GetKlijenta(GlobalClient.prijavljeniKlijentId);
            var hash = GenerateHash(klijent.LozinkaSalt, model.TrenutnaLozinka);
            if (hash != klijent.LozinkaHash)
            {
                ModelState.AddModelError("TrenutnaLozinka", "Niste unijeli ispravnu trenutnu lozinku.");
                viewModel = new ClientChangePasswordViewModel();
                return View(viewModel);
            }
            
                var noviKlijent = new KlijentiInsertRequest()
                {
                    Ime = klijent.Ime,
                    Prezime = klijent.Prezime,
                    Email = klijent.Email,
                    Telefon = klijent.Telefon,
                    KorisnickoIme = klijent.KorisnickoIme,
                    GradId = klijent.GradId,
                    Slika = klijent.Slika,
                    Password = model.NovaLozinka,
                    PasswordConfirmation = model.PotvrdaLozinke
                };
                _klijentiService.Update(klijent.KlijentId, noviKlijent);

                return RedirectToAction("Login","Login");
        }
        [HttpGet]
        public IActionResult PromjeniEmail()
        {
            var klijent = _klijentiService.GetById(GlobalClient.prijavljeniKlijentId);
            var model = new ClientChangeEmailViewModel() { 
                TrenutniEmail=klijent.Email
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PromjeniEmail(ClientChangeEmailViewModel model)
        {
            var klijent = _klijentiService.GetKlijenta(GlobalClient.prijavljeniKlijentId);
            if (ModelState.IsValid)
            {
                klijent.Email = model.NoviEmail;
                _klijentiService.UpdateBanovan(klijent.KlijentId, klijent);
                TempData["emailPoruka"] = "Email uspjesno izmjenjen!";
                return RedirectToAction(nameof(ProfilKlijenta));
            }
            var viewModel = new ClientChangeEmailViewModel()
            {
                TrenutniEmail = klijent.Email
            };
            return View(viewModel);
        }
        public IActionResult BanKlijenta(int id)
        {
            var klijent = _klijentiService.GetKlijenta(id);
            klijent.Banovan = true;
            _klijentiService.UpdateBanovan(klijent.KlijentId, klijent);
            TempData["klijentBanovan"] = "Klijent uspjesno banovan!";
            return RedirectToAction("DetaljiKlijenta",new {id=klijent.KlijentId });
        }
        public IActionResult UnbanKlijenta(int id)
        {
            var klijent = _klijentiService.GetKlijenta(id);
            klijent.Banovan = false;
            _klijentiService.UpdateBanovan(klijent.KlijentId, klijent);
            TempData["klijentUnbanovan"] = "Klijent uspjesno unbanovan!";
            // return RedirectToAction(nameof(ListaKlijenata));
            return RedirectToAction("DetaljiKlijenta", new { id = klijent.KlijentId });

        }
        [HttpGet]
        public IActionResult ListaTermina()
        {
            var model = new List<ListOfAppointmentsViewModel>();
            var lista = _terminiService.Get(new TerminiSearchRequest
            {
                Aktivan = true
            });
            foreach(var item in lista)
            {
                model.Add(new ListOfAppointmentsViewModel()
                {
                    TerminId=item.TerminId,
                    Datum = item.Datum,
                    VrijemeDo = item.VrijemeDo,
                    VrijemeOd = item.VrijemeOd
                });
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult RezervisiTermin(int id)
        {
            var termin = _terminiService.GetById(id);
            var model = new ClientAppointmentViewModel()
            {
                TerminId = termin.TerminId,
                Datum = termin.Datum.ToShortDateString(),
                VrijemeDo = termin.VrijemeDo.ToString(@"hh\:mm"),
                VrijemeOd = termin.VrijemeOd.ToString(@"hh\:mm")
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult RezervisiTermin(ClientAppointmentViewModel model)
        {
            var termin = _terminiService.GetById(model.TerminId);

            var rezervacija = new Model.RezervacijeGluveSobe()
            {
                Datum = termin.Datum,
                VrijemeDo = termin.VrijemeDo,
                VrijemeOd = termin.VrijemeOd,
                KlijentId = GlobalClient.prijavljeniKlijentId
            };
            _rezervacijeGluveSobe.Insert(rezervacija);
            var noviTermin = new TerminiInsertRequest()
            {
                Aktivan = false,
                Datum = termin.Datum,
                VrijemeDo = termin.VrijemeDo,
                VrijemeOd = termin.VrijemeOd,
                KorisnikId=termin.KorisnikId
            };
            _terminiService.Update(termin.TerminId, noviTermin);
            TempData["termin"] = "Termin uspjesno rezervisan!";
            return RedirectToAction(nameof(ListaTermina));
        }
        public IActionResult Logout()
        {
            GlobalClient.prijavljeniKlijent = null;
            GlobalClient.prijavljeniKlijentId = -1;
            return RedirectToAction("Login","Login");
        }
    }
}
