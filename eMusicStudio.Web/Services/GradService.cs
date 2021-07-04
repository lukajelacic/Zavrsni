using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eMusicStudio.Web.Models;

namespace eMusicStudio.Web.Services
{
    public class GradService : BaseService<Model.Grad, object, Models.Grad>
    {
        public GradService(_150192V1Context context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
