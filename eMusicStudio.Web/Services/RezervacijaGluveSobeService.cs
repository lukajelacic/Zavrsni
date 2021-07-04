using AutoMapper;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public class RezervacijaGluveSobeService : BaseCRUDService<Model.RezervacijeGluveSobe, RezervacijaGluveSobeSearchRequest, Models.RezervacijeGluveSobe, Model.RezervacijeGluveSobe, Model.RezervacijeGluveSobe>
    {
        public RezervacijaGluveSobeService(_150192V1Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public override List<Model.RezervacijeGluveSobe> Get(RezervacijaGluveSobeSearchRequest search)
        {
            //var list = _context.Set<Database.RezervacijeGluveSobe>().Where(x=>x.KlijentId==search.KlijentId).ToList();
            var query = _context.Set<Models.RezervacijeGluveSobe>().AsQueryable();
            if (search?.KlijentId.HasValue == true)
            {
                query = query.Where(x => x.KlijentId == search.KlijentId);
            }
            query = query.OrderBy(x => x.Datum);
            var list = query.ToList();
            return _mapper.Map<List<Model.RezervacijeGluveSobe>>(list);
        }
    }
}
