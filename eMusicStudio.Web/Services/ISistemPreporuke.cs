using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public interface ISistemPreporuke
    {
        List<Model.MuzickaOprema> Get(int id);
    }
}
