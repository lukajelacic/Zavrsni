using AutoMapper;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Models;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public class TerminiService : BaseCRUDService<Model.Termini, TerminiSearchRequest, Models.Termini, TerminiInsertRequest, TerminiInsertRequest>
    {
        public TerminiService(_150192V1Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public override List<Model.Termini> Get(TerminiSearchRequest search)
        {
            var query = _context.Set<Models.Termini>().AsQueryable();
            if (search?.Datum.HasValue == true)
            {
                DateTime date = new DateTime();
                date = search.Datum.Value;
                query = query.Where(x => x.Datum.Date.CompareTo(date.Date) == 0);
            }
            if (search?.Aktivan.HasValue == true)
            {
                query = query.Where(x => x.Aktivan == true);
            }
            var list = query.Where(x=>x.Aktivan==true).ToList();
            return _mapper.Map<List<Model.Termini>>(list);

        }
    }
}
