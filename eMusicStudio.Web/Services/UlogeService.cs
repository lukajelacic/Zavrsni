using AutoMapper;
using eMusicStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public class UlogeService : BaseService<Model.Uloge, object, Models.Uloge>
    {
        public UlogeService(_150192V1Context context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
