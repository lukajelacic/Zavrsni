using eMusicStudio.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public interface IRezervacijeStavkeService
    {
        List<Model.RezervacijaStavke> Get(RezervacijaStavkeSearchRequest request);
        Model.RezervacijaStavke Insert(Model.RezervacijaStavke request);

    }
}
