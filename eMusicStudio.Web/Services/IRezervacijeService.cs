using eMusicStudio.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public interface IRezervacijeService
    {
        List<Model.Rezervacija> Get(RezervacijeSearchRequest request);
        Model.Rezervacija GetById(int id);
        Model.Rezervacija Insert(RezervacijeUpsertRequest request);
        Model.Rezervacija Update(int id,RezervacijeUpsertRequest request);
        bool Delete(int id);
    }
}
