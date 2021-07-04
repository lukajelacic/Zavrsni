using AutoMapper;
using eMusicStudio.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public class BaseService<TModel, TSearch, TDatabase> : IService<TModel, TSearch> where TDatabase:class
    {
        protected readonly _150192V1Context _context;
        protected readonly IMapper _mapper;
        public BaseService(_150192V1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual List<TModel> Get(TSearch search)
        {
            var list = _context.Set<TDatabase>().ToList();
            return _mapper.Map<List<TModel>>(list);
        }

        public virtual TModel GetById(int id)
        {
            var result = _context.Set<TDatabase>().Find(id);
            return _mapper.Map<TModel>(result);
        }
    }
}
