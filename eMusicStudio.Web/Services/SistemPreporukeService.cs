using AutoMapper;
using eMusicStudio.Model;
using eMusicStudio.Web.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Services
{
    public class SistemPreporukeService : ISistemPreporuke
    {
        private readonly _150192V1Context _context;
        private readonly IMapper _mapper;
        Dictionary<int, List<Models.Ocjene>> proizvodi = new Dictionary<int, List<Models.Ocjene>>();
        public SistemPreporukeService(_150192V1Context context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.MuzickaOprema> Get(int id)
        {
            UcitajOpremu(id);
            List<Models.Ocjene> ocjenePosmatranogInstrumenta = _context.Ocjene.Where(x => x.MuzickaOpremaId == id).OrderBy(x => x.KlijentId).ToList();
            List<Models.Ocjene> zajednickeOcjene1 = new List<Models.Ocjene>();
            List<Models.Ocjene> zajednickeOcjene2 = new List<Models.Ocjene>();
            List<Model.MuzickaOprema> preporuceniProizvodi = new List<Model.MuzickaOprema>();
            foreach(var item in proizvodi)
            {
                foreach(var o in ocjenePosmatranogInstrumenta)
                {
                    if (item.Value.Where(x => x.KlijentId == o.KlijentId).Count() > 0)
                    {
                        zajednickeOcjene1.Add(o);
                        zajednickeOcjene2.Add(item.Value.Where(x => x.KlijentId == o.KlijentId).First());
                    }
                }
                double slicnost = GetSlicnost(zajednickeOcjene1, zajednickeOcjene2);
                if (slicnost > 0.5)
                {
                    var temp = _context.MuzickaOprema.Find(item.Key);
                    Model.MuzickaOprema opremaTemp = _mapper.Map<Model.MuzickaOprema>(temp);
                    preporuceniProizvodi.Add(opremaTemp);
                }
                zajednickeOcjene1.Clear();
                zajednickeOcjene2.Clear();
            }

            return preporuceniProizvodi;
        }

        private double GetSlicnost(List<Models.Ocjene> zajednickeOcjene1, List<Models.Ocjene> zajednickeOcjene2)
        {
            if (zajednickeOcjene1.Count != zajednickeOcjene2.Count)
                return 0;
            double brojnik = 0, nazivnik1 = 0, nazivnik2 = 0;

            for (int i = 0; i < zajednickeOcjene1.Count; i++)
            {
                brojnik = zajednickeOcjene1[i].Ocjena * zajednickeOcjene2[i].Ocjena;
                nazivnik1 = zajednickeOcjene1[i].Ocjena * zajednickeOcjene1[i].Ocjena;
                nazivnik2 = zajednickeOcjene2[i].Ocjena * zajednickeOcjene2[i].Ocjena;
            }
            nazivnik1 = Math.Sqrt(nazivnik1);
            nazivnik2 = Math.Sqrt(nazivnik2);
            double nazivnik = nazivnik1 * nazivnik2;
            if (nazivnik == 0)
            {
                return 0;
            }
            else
            {
                return brojnik / nazivnik;
            }
        }

        private void UcitajOpremu(int id)
        {
            List<Models.MuzickaOprema> muzickaOprema = _context.MuzickaOprema.Where(x => x.MuzickaOpremaId != id).ToList();
            List<Models.Ocjene> ocjene;
            foreach(Models.MuzickaOprema item in muzickaOprema)
            {
                ocjene = _context.Ocjene.Where(x => x.MuzickaOpremaId == item.MuzickaOpremaId).OrderBy(x => x.KlijentId).ToList();
                if (ocjene.Count > 0)
                    proizvodi.Add(item.MuzickaOpremaId, ocjene);
            }
        }
    }
}
