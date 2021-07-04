using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMusicStudio.Model;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Services;
using eMusicStudio.Web.ViewModels.Rezervacije;
using Microsoft.AspNetCore.Mvc;

namespace eMusicStudio.Web.Controllers
{
    public class RezervacijeController : Controller
    {
        private readonly IRezervacijeService _rezervacijeService;
        private readonly IRezervacijeStavkeService _rezervacijaStavkeService;
        private readonly ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> _muzickaOpremaService;
        public RezervacijeController(IRezervacijeService rezervacijeService,IRezervacijeStavkeService rezervacijaStavkeService, ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> muzickaOpremaService)
        {
            _rezervacijeService = rezervacijeService;
            _rezervacijaStavkeService = rezervacijaStavkeService;
            _muzickaOpremaService = muzickaOpremaService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ListaRezervacija(int? id)
        {
            ArhiviraneRezervacije();
            var rezervacije = new List<Rezervacija>();
            if (id.HasValue)
            {
                rezervacije = _rezervacijeService.Get(new RezervacijeSearchRequest()
                {
                    KlijentId = id.Value
                });
            }
            else
            {
                rezervacije = _rezervacijeService.Get(null);
            }

            var model = new List<ClientsReservationsViewModel>();
            foreach (var item in rezervacije)
            {
                model.Add(new ClientsReservationsViewModel()
                {
                    RezervacijaId = item.RezervacijaId,
                    BrojRezervacije = item.BrojRezervacije,
                    DatumRezervacije = item.DatumRezervacije,
                    Cijena = item.Cijena
                });
            }
            return View(model);
        }
        public void  ArhiviraneRezervacije()
        {
            var listaRezervacija = _rezervacijeService.Get(null);
            var novaLista = new List<Model.Rezervacija>();
            foreach (var item in listaRezervacija)
            {
                if (item.DatumRezervacije.Date < DateTime.Now.Date && item.Arhivirana == false)
                {
                    novaLista.Add(item);
                }
            }
            foreach (var item in novaLista)
            {
                var stavke = _rezervacijaStavkeService.Get(new RezervacijaStavkeSearchRequest()
                {
                    RezervacijaId = item.RezervacijaId
                });
                foreach (var item2 in stavke)
                {
                    var muzickaOprema = _muzickaOpremaService.GetById(item2.MuzickaOpremaId);
                    muzickaOprema.NaStanju += item2.Kolicina;
                    var request = new MuzickaOpremaUpsertRequest()
                    {
                        Cijena = muzickaOprema.Cijena,
                        VrstaId = muzickaOprema.VrstaId,
                        NaStanju = muzickaOprema.NaStanju,
                        Naziv = muzickaOprema.Naziv,
                        Slika = muzickaOprema.Slika
                    };
                    _muzickaOpremaService.Update(muzickaOprema.MuzickaOpremaId, request);
                }
                var request1 = new RezervacijeUpsertRequest()
                {
                    Arhivirana = true,
                    BrojRezervacije = item.BrojRezervacije,
                    Cijena = item.Cijena,
                    Datum = item.Datum,
                    DatumRezervacije = item.DatumRezervacije,
                    KlijentId = item.KlijentId,
                    Otkazano = item.Otkazano,
                    KorisnikId = item.KorisnikId,
                    Status = item.Status
                };
                _rezervacijeService.Update(item.RezervacijaId, request1);
            }
        }
        [HttpGet]
        public IActionResult RezervacijeKlijenta()
        {
            var list = _rezervacijeService.Get(new RezervacijeSearchRequest()
            {
                KlijentId = GlobalClient.prijavljeniKlijentId
            });
            var model = new List<RezervacijeKlijentaViewModel>();
            foreach (var item in list)
            {
                model.Add(new RezervacijeKlijentaViewModel()
                {
                    RezervacijaId = item.RezervacijaId,
                    BrojRezervacije = item.BrojRezervacije,
                    DatumRezervacije = item.DatumRezervacije.ToShortDateString(),
                    Cijena = item.Cijena.Value
                });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult DetaljiRezervacije(int id)
        {
            var rezervacija = _rezervacijeService.GetById(id);
            var rezervacijaStavke = _rezervacijaStavkeService.Get(new RezervacijaStavkeSearchRequest()
            {
                RezervacijaId = rezervacija.RezervacijaId
            });
            var rezervacijeStavkeRez = new List<RezervacijaStavkeRezultat>();
            foreach (var item in rezervacijaStavke)
            {
                RezervacijaStavkeRezultat temp = new RezervacijaStavkeRezultat(item);
                rezervacijeStavkeRez.Add(temp);
                temp = null;
            }
            var model = new ViewModels.Rezervacije.DetaljiRezervacijeViewModel()
            {
                RezervacijaId = rezervacija.RezervacijaId,
                BrojRezervacije = rezervacija.BrojRezervacije,
                Cijena = rezervacija.Cijena.Value,
                DatumRezervacije = rezervacija.DatumRezervacije,
                RezervacijeStavke = rezervacijeStavkeRez,
                Otkazana=rezervacija.Otkazano.Value,
            };
            return View(model);
        }
        public IActionResult OtkaziRezervaciju(int id)
        {
            var rezervacija = _rezervacijeService.GetById(id);
            var model = new Model.Requests.RezervacijeUpsertRequest()
            {
                BrojRezervacije = rezervacija.BrojRezervacije,
                Status = rezervacija.Status,
                Arhivirana = rezervacija.Arhivirana,
                Cijena = rezervacija.Cijena,
                Datum = rezervacija.Datum,
                DatumRezervacije = rezervacija.DatumRezervacije,
                KlijentId = rezervacija.KlijentId,
                Otkazano = true
            };
            var rezervacijaStavke = _rezervacijaStavkeService.Get(new RezervacijaStavkeSearchRequest()
            {
                RezervacijaId = id
            });
            _rezervacijeService.Update(rezervacija.RezervacijaId, model);
            foreach (var item in rezervacijaStavke)
            {
                var oprema = _muzickaOpremaService.GetById(item.MuzickaOpremaId);
                oprema.NaStanju += item.Kolicina;
                var updateOprema = new MuzickaOpremaUpsertRequest()
                {
                    Naziv = oprema.Naziv,
                    NaStanju = oprema.NaStanju,
                    Cijena = oprema.Cijena,
                    Slika = oprema.Slika,
                    VrstaId = oprema.VrstaId
                };
                _muzickaOpremaService.Update(oprema.MuzickaOpremaId, updateOprema);
            }
            TempData["rezOtkazana"] = "Rezervacija je uspjesno otkazana!";
            return RedirectToAction(nameof(RezervacijeKlijenta));
        }
        [HttpGet]
        public IActionResult RezervisiMuzickiInstrument(int id)
        {
            var instrument = _muzickaOpremaService.GetById(id);
            var model = new RezervacijaViewModel()
            {
                MuzickaOprema = instrument,
                Kolicina = 0
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult RezervisiMuzickiInstrument(RezervacijaViewModel model)
        {
            if (CartService.Cart.ContainsKey(model.MuzickaOprema.MuzickaOpremaId))
            {
                CartService.Cart.Remove(model.MuzickaOprema.MuzickaOpremaId);
            }
            CartService.Cart.Add(model.MuzickaOprema.MuzickaOpremaId, model);
            // prebaciti da vrsi redirect na listu muzickih instrumenata
            return RedirectToAction(nameof(TrenutnaRezervacija));
        }
        [HttpGet]
        public IActionResult TrenutnaRezervacija()
        {
            var model = new List<RezervacijaViewModel>();
            foreach (var item in CartService.Cart.Values)
            {
                model.Add(item);
            }
            return View(model);
        }
        public IActionResult PovecajKolicinu(int id)
        {
            var model = CartService.Cart.FirstOrDefault(x => x.Key == id).Value;
            model.Kolicina += 1;
            if (CartService.Cart.ContainsKey(id))
            {
                CartService.Cart.Remove(id);
            }
            CartService.Cart.Add(id, model);
            return RedirectToAction(nameof(TrenutnaRezervacija));

        }
        public IActionResult SmanjiKolicinu(int id)
        {
            var model = CartService.Cart.FirstOrDefault(x => x.Key == id).Value;
            model.Kolicina -= 1;
            if (CartService.Cart.ContainsKey(id))
            {
                CartService.Cart.Remove(id);
            }
            CartService.Cart.Add(id, model);
            return RedirectToAction(nameof(TrenutnaRezervacija));
        }
        public IActionResult IzbrisiInstrument(int id)
        {
            if (CartService.Cart.ContainsKey(id))
            {
                CartService.Cart.Remove(id);
            }
            return RedirectToAction(nameof(TrenutnaRezervacija));
        }
        public IActionResult ZakljuciRezervaciju(DateTime Datum)
        {
            if (DateTime.Now.Date > Datum.Date)
            {
                return Content("Datum mora biti veci od danasnjeg datuma.");
            }
            int cijena = 0;
            int brojRezervacije;
            var list = _rezervacijeService.Get(null);
            if (list.Count == 0)
            {
                brojRezervacije = 1;
            }
            else
            {
                brojRezervacije = list.Select(x => x.BrojRezervacije).Max() + 1;
            }
            foreach (var item in CartService.Cart.Values)
            {
                cijena += item.Kolicina * item.MuzickaOprema.Cijena;
            }
            var rezervacija = new Model.Requests.RezervacijeUpsertRequest()
            {
                BrojRezervacije = brojRezervacije,
                Datum = DateTime.Now,
                DatumRezervacije = Datum,
                Otkazano = false,
                Status = false,
                KlijentId = GlobalClient.prijavljeniKlijentId,
                Cijena = cijena,
                Arhivirana = false
            };
            var rez = _rezervacijeService.Insert(rezervacija);

            List<Model.RezervacijaStavke> rezervacijaStavke = new List<Model.RezervacijaStavke>();
            foreach (var item in CartService.Cart.Values)
            {
                rezervacijaStavke.Add(new Model.RezervacijaStavke()
                {
                    RezervacijaId = rez.RezervacijaId,
                    MuzickaOpremaId = item.MuzickaOprema.MuzickaOpremaId,
                    Kolicina = item.Kolicina
                });
            }
            bool uspjesna = true;
            foreach (var item in rezervacijaStavke)
            {
                var oprema = _muzickaOpremaService.GetById(item.MuzickaOpremaId);
                if (item.Kolicina > oprema.NaStanju)
                {
                    uspjesna = false;
                    _rezervacijeService.Delete(rez.RezervacijaId);
                    return Content("Nazalost nemamo toliko " + oprema.Naziv + " na stanju.");
                }
                else
                {
                    _rezervacijaStavkeService.Insert(item);
                    oprema.NaStanju -= item.Kolicina;
                    var updateOprema = new MuzickaOpremaUpsertRequest()
                    {
                        Naziv = oprema.Naziv,
                        NaStanju = oprema.NaStanju,
                        Cijena = oprema.Cijena,
                        Slika = oprema.Slika,
                        VrstaId = oprema.VrstaId
                    };
                    _muzickaOpremaService.Update(oprema.MuzickaOpremaId, updateOprema);
                }
            }
            CartService.Cart.Clear();
            if (!uspjesna)
            {
                return Content("Nismo uspjeli procesirati rezervaciju, pokusajte opet kasnije.");
            }
            else
            {
                TempData["rez"] = "Rezervacija uspjesno kreirana!";
                return RedirectToAction("DetaljiRezervacije",new {id=rez.RezervacijaId });
            }
        }
    }
}
