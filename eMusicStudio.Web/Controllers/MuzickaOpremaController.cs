using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Services;
using eMusicStudio.Web.ViewModels.MuzickaOprema;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eMusicStudio.Web.Controllers
{
    public class MuzickaOpremaController : Controller
    {
        private readonly IService<Model.Vrsta, object> _vrstaService;
        private readonly ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> _muzickaOpremaService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MuzickaOpremaController(IService<Model.Vrsta, object> vrstaService, ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> muzickaOpremaService,
         IWebHostEnvironment webHostEnvironment)
        {
            _vrstaService = vrstaService;
            _muzickaOpremaService = muzickaOpremaService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DodajMuzickiInstrument()
        {
            var list = _vrstaService.Get(null);
            var listaVrsta = new List<SelectListItem>();
            foreach (var item in list)
            {
                listaVrsta.Add(new SelectListItem()
                {
                    Value = item.VrstaId.ToString(),
                    Text = item.Naziv
                });
            };
            var model = new AddNewMusicInstrumentViewModel();
            model.Vrste = listaVrsta;
            return View(model);
        }
        [HttpPost]
        public IActionResult DodajMuzickiInstrument(AddNewMusicInstrumentViewModel model)
        {
            string uniqueFileName = Helper.Image.Upload(model.Slika, _webHostEnvironment);
            if (ModelState.IsValid)
            {
                var instrument = new MuzickaOpremaUpsertRequest()
                {
                    Naziv = model.Naziv,
                    NaStanju = model.NaStanju,
                    Slika = uniqueFileName,
                    Cijena = model.Cijena,
                    VrstaId = model.VrstaId
                };
                _muzickaOpremaService.Insert(instrument);
                TempData["Success"] = "Uspjesno dodan novi instrument!";
                return RedirectToAction(nameof(ListaInstrumenata));
            }
            var list = _vrstaService.Get(null);
            var listaVrsta = new List<SelectListItem>();
            foreach (var item in list)
            {
                listaVrsta.Add(new SelectListItem()
                {
                    Value = item.VrstaId.ToString(),
                    Text = item.Naziv
                });
            };
            var viewModel = new AddNewMusicInstrumentViewModel();
            viewModel.Vrste = listaVrsta;
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ListaInstrumenata()
        {
            var lista = _muzickaOpremaService.Get(null);
            var list = _vrstaService.Get(null);
            var listaVrsta = new List<SelectListItem>();
            foreach (var item in list)
            {
                listaVrsta.Add(new SelectListItem()
                {
                    Value = item.VrstaId.ToString(),
                    Text = item.Naziv
                });
            };
            var model = new MusicInstrumentsViewModel();
            model.muzickaOprema = lista;
            model.Vrste = listaVrsta;
            if (GlobalUser.prijavljeniKorisnik != null)
                model.isAdmin = true;
            else
                model.isAdmin = false;
            return View(model);
        }
        [HttpPost]
        public IActionResult ListaInstrumenata(int VrstaId)
        {
            var lista = _muzickaOpremaService.Get(new MuzickaOpremaSearchRequest()
            {
                VrstaId = VrstaId
            });
            var list = _vrstaService.Get(null);
            var listaVrsta = new List<SelectListItem>();
            foreach (var item in list)
            {
                listaVrsta.Add(new SelectListItem()
                {
                    Value = item.VrstaId.ToString(),
                    Text = item.Naziv
                });
            };
            var model = new MusicInstrumentsViewModel();
            model.muzickaOprema = lista;
            model.Vrste = listaVrsta;
            if (GlobalUser.prijavljeniKorisnik != null)
                model.isAdmin = true;
            else
                model.isAdmin = false;
            return View(model);
        }

        // moze samo admin
        [HttpGet]
        public IActionResult PovecajBrojNaStanju(int id)
        {
            var instrument = _muzickaOpremaService.GetById(id);

            var model = new NumberInStockViewModel()
            {
                MuzickaOpremaId = instrument.MuzickaOpremaId,
                Naziv = instrument.Naziv,
                NaStanju = instrument.NaStanju
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PovecajBrojNaStanju(NumberInStockViewModel model)
        {
            var instrument = _muzickaOpremaService.GetById(model.MuzickaOpremaId);
            if (ModelState.IsValid)
            {
                instrument.NaStanju += model.Kolicina;
                var novi = new MuzickaOpremaUpsertRequest()
                {
                    Naziv = instrument.Naziv,
                    NaStanju = instrument.NaStanju,
                    Slika = instrument.Slika,
                    Cijena = instrument.Cijena,
                    VrstaId = instrument.VrstaId
                };
                _muzickaOpremaService.Update(instrument.MuzickaOpremaId, novi);
                TempData["poruka-sucess"] = "Uspjesno povecan broj na stanju!";
                return RedirectToAction(nameof(ListaInstrumenata));
            }
            var viewModel = new NumberInStockViewModel()
            {
                MuzickaOpremaId = instrument.MuzickaOpremaId,
                Naziv = instrument.Naziv,
                NaStanju = instrument.NaStanju
            };
            return View(viewModel);
        }
    }
}
