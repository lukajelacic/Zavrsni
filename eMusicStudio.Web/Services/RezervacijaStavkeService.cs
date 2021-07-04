using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eMusicStudio.Model;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace eMusicStudio.Web.Services
{
    public class RezervacijaStavkeService : IRezervacijeStavkeService 
    {
       
        private readonly _150192V1Context _context;
        private readonly IMapper _mapper;
        public RezervacijaStavkeService(_150192V1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public  List<Model.RezervacijaStavke> Get(RezervacijaStavkeSearchRequest request)
        {
            if (request == null)
            {
                var lista = _context.RezervacijaStavke.Include(x => x.MuzickaOprema).ToList();
                return _mapper.Map<List<Model.RezervacijaStavke>>(lista);
            }
            var list = _context.RezervacijaStavke.Include(x=>x.MuzickaOprema).Where(x => x.RezervacijaId == request.RezervacijaId).ToList();
            return _mapper.Map<List<Model.RezervacijaStavke>>(list);
        }

        public Model.RezervacijaStavke Insert(Model.RezervacijaStavke request)
        {
            var entity = _mapper.Map<Models.RezervacijaStavke>(request);
            _context.RezervacijaStavke.Add(entity);
            _context.SaveChanges();
            return _mapper.Map<Model.RezervacijaStavke>(entity);
        }
    }
}
