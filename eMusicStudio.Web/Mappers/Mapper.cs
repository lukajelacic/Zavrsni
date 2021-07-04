using AutoMapper;
using eMusicStudio.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Mappers
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Models.Korisnici, Model.Korisnici>().ReverseMap();
            CreateMap<Models.Korisnici, KorisniciInsertRequest>().ReverseMap();
            CreateMap<Models.Uloge, Model.Uloge>().ReverseMap();
            CreateMap<Models.Klijenti, Model.Klijenti>().ReverseMap();
            CreateMap<Models.Klijenti, KlijentiInsertRequest>().ReverseMap();
            CreateMap<Models.MuzickaOprema, MuzickaOpremaInsertRequest>().ReverseMap();
            CreateMap<Models.Vrsta, Model.Vrsta>().ReverseMap();
            CreateMap<Models.Vrsta, VrstaInsertRequest>().ReverseMap();
            CreateMap<Models.MuzickaOprema, Model.MuzickaOprema>().ReverseMap();
            CreateMap<Models.MuzickaOprema, MuzickaOpremaUpsertRequest>().ReverseMap();
            CreateMap<Models.Rezervacije, Model.Rezervacija>().ReverseMap();
            CreateMap<Models.Rezervacije, RezervacijeUpsertRequest>().ReverseMap();
            CreateMap<Models.RezervacijaStavke, Model.RezervacijaStavke>().ReverseMap();
            CreateMap<Models.Termini, Model.Termini>().ReverseMap();
            CreateMap<Models.Termini, TerminiInsertRequest>().ReverseMap();
            CreateMap<Models.RezervacijeGluveSobe, Model.RezervacijeGluveSobe>().ReverseMap();
            CreateMap<Models.Ocjene, Model.Ocjene>().ReverseMap();
            CreateMap<Models.Ocjene, OcjenaInsertRequest>().ReverseMap();
            CreateMap<Models.Klijenti, Model.Requests.KlijentBanovanRequest>().ReverseMap();
            CreateMap<Models.Grad, Model.Grad>().ReverseMap();


        }
    }
}
