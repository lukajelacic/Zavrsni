using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace eMusicStudio.Web
{
    public class Program
    {
        private  static IRezervacijeService _rezervacije;
        private static IRezervacijeStavkeService _rezervacijaStavke;
        private  static ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> _termini;
        private static ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> _muzickaOprema;
        public Program(IRezervacijeService rezervacije,IRezervacijeStavkeService rezervacijeStavke, ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest, MuzickaOpremaUpsertRequest> muzickaOprema, ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest> termini)
        {
            _rezervacije = rezervacije;
            _rezervacijaStavke = rezervacijeStavke;
            _muzickaOprema = muzickaOprema;
            _termini = termini;

        }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
       
    }
}
