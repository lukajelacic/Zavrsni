using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace eMusicStudio.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IKlijentiService _klijentiService;
        private readonly IRezervacijeService _rezervacijeService;
        private readonly ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> _muzickaOpremaService;
        private readonly IRezervacijeStavkeService _rezervacijeStavkeService;
        public ReportController(IKlijentiService klijentiService,IRezervacijeService rezervacijeService, ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> muzickaOpremaService,IRezervacijeStavkeService rezervacijeStavkeService)
        {
            _klijentiService = klijentiService;
            _rezervacijeService = rezervacijeService;
            _muzickaOpremaService = muzickaOpremaService;
            _rezervacijeStavkeService = rezervacijeStavkeService;
        }
        public IActionResult Index()
        {
            
            LocalReport _localReport = new LocalReport("Reports/Klijenti.rdlc");
            var podaci = Init();
            _localReport.AddDataSource("DataSet1", podaci);
            ReportResult result = _localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
        public ReportViewModel KreirajModel(int id)
        {
            var klijent = _klijentiService.GetById(id);
            var rezervacije = _rezervacijeService.Get(new Model.Requests.RezervacijeSearchRequest()
            {
                KlijentId = id
            });
            decimal? potroseno = 0;
            foreach(var item in rezervacije)
            {
                potroseno += item.Cijena;
            }
            var model = new ReportViewModel()
            {
                ImePrezime = klijent.Ime + " " + klijent.Prezime,
                KorisnickoIme = klijent.KorisnickoIme,
                BrojRezervacija = rezervacije.Count,
                Potroseno = potroseno.Value
            };
            return model;
        }

        public  List<ReportViewModel> Init()
        {
            var lista = _klijentiService.Get(null);
            var list = new List<ReportViewModel>();
            foreach(var item in lista)
            {
                list.Add(KreirajModel(item.KlijentId));
            }
            return list;
        }
        public IActionResult Instrumenti()
        {
            LocalReport _localReport = new LocalReport("Reports/Instrumenti.rdlc");
            var podaci = InitInstrumenti();
            _localReport.AddDataSource("DataSet1", podaci);
            ReportResult result = _localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
        public InstrumentiViewModel KreirajInstrument(int id)
        {
            var muzickaOprema = _muzickaOpremaService.GetById(id);
            var stavke = _rezervacijeStavkeService.Get(null);
            int brojRezervacija = 0;
            decimal zaradjeno = 0;
            foreach(var item in stavke)
            {
                if (item.MuzickaOpremaId == muzickaOprema.MuzickaOpremaId)
                {
                    brojRezervacija += item.Kolicina;
                    zaradjeno += item.Kolicina * muzickaOprema.Cijena;
                }
            };
            var model = new InstrumentiViewModel()
            {
                Naziv = muzickaOprema.Naziv,
                BrojRezervacija = brojRezervacija,
                Zarada = zaradjeno
            };
            return model;
        }
        public List<InstrumentiViewModel> InitInstrumenti()
        {
            var lista = _muzickaOpremaService.Get(null);
            var list = new List<InstrumentiViewModel>();
            foreach(var item in lista)
            {
                list.Add(KreirajInstrument(item.MuzickaOpremaId));
            }
            return list;
        }
    }
}
