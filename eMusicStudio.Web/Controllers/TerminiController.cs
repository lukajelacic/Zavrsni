using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Services;
using eMusicStudio.Web.ViewModels.Termini;
using Microsoft.AspNetCore.Mvc;

namespace eMusicStudio.Web.Controllers
{
    public class TerminiController : Controller
    {
        private readonly ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> _terminiService;
        public TerminiController(ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> terminiService)
        {
            _terminiService = terminiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DodajTermin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DodajTermin(CreateAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var termin = new TerminiInsertRequest()
                {
                    Datum = model.Datum.Value,
                    VrijemeOd = model.VrijemeOd.Value,
                    VrijemeDo = model.VrijemeDo.Value,
                    Aktivan=true,
                    KorisnikId = GlobalUser.prijavljeniKorisnikId
                };
                _terminiService.Insert(termin);
                TempData["termin"] = "Uspjesno dodan novi termin!";
                return RedirectToAction(nameof(ListaTermina));
            }

            return View();
        }
        [HttpGet]
        public IActionResult ListaTermina()
        {
            AktivniTermini();
            var list = _terminiService.Get(null);
            //var model = new List<CreateAppointmentViewModel>();
            var model = new ListOfAllAppointmentsViewModel();
            var lista = new List<CreateAppointmentViewModel>();
            foreach (var item in list)
            {
                lista.Add(new CreateAppointmentViewModel()
                {
                    Datum = item.Datum,
                    VrijemeDo = item.VrijemeDo,
                    VrijemeOd = item.VrijemeOd,
                    KorisnikId = item.KorisnikId
                });
            }
            model.Lista = lista;
            if (GlobalUser.prijavljeniKorisnik != null)
                model.IsAdmin = true;
            else
                model.IsAdmin = false;
            return View(model);
        }
        public  void AktivniTermini()
        {
            var list =  _terminiService.Get(null);
            var nova = new List<Model.Termini>();
            foreach (var item in list)
            {
                if (item.Datum.Date < DateTime.Now.Date)
                {
                    nova.Add(item);
                }
            }
            foreach (var item in nova)
            {
                var request = new TerminiInsertRequest()
                {
                    Aktivan = false,
                    Datum = item.Datum,
                    KorisnikId = item.KorisnikId,
                    VrijemeDo = item.VrijemeDo,
                    VrijemeOd = item.VrijemeOd
                };
                _terminiService.Update(item.TerminId, request);
            }
        }
    }
}
