using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public interface IMuzickaOpremaService
    {
        List<Model.MuzickaOprema> Get(MuzickaOpremaSearchRequest request);
        Model.MuzickaOprema GetById(int id);
        Model.MuzickaOprema Insert(MuzickaOpremaInsertRequest request);
    }
}
