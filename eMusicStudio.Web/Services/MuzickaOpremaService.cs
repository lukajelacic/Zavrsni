using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Models;

namespace eMusicStudio.Web.Services
{
    public class MuzickaOpremaService : BaseCRUDService<Model.MuzickaOprema,MuzickaOpremaSearchRequest,Models.MuzickaOprema,MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest>
    {
        public MuzickaOpremaService(_150192V1Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public override List<Model.MuzickaOprema> Get(MuzickaOpremaSearchRequest search)
        {
            var query = _context.Set<Models.MuzickaOprema>().AsQueryable();
            if (search?.VrstaId.HasValue == true)
            {
                query = query.Where(x => x.VrstaId == search.VrstaId);
            }
            query = query.OrderBy(x => x.Naziv);
            var list = query.ToList();
            return _mapper.Map<List<Model.MuzickaOprema>>(list);
        }

    }
}
