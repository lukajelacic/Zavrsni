using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Filters;
using eMusicStudio.Web.Mappers;
using eMusicStudio.Web.Models;
using eMusicStudio.Web.Security;
using eMusicStudio.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eMusicStudio.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            
            services.AddMvc(/*x => x.Filters.Add<ErrorFilter>()*/).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });
            // services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, /BasicAuthenticationHandler>("BasicAuthentication", null);
            //services
            //    .AddAuthentication()
            //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            //});


            services.AddScoped<IKorisniciService, KorisniciService>();
            services.AddScoped<IKlijentiService, KlijentiService>();
            services.AddScoped<IService<Model.Vrsta, object>, BaseService<Model.Vrsta, object, Models.Vrsta>>();
            services.AddScoped<ICRUDService<Model.MuzickaOprema, MuzickaOpremaSearchRequest, MuzickaOpremaUpsertRequest,  MuzickaOpremaUpsertRequest>, MuzickaOpremaService>();
            services.AddScoped<IService<Model.Grad, object>, GradService>();
            services.AddScoped<IRezervacijeService, RezervacijeService>();
            services.AddScoped<IRezervacijeStavkeService, RezervacijaStavkeService>();
            services.AddScoped<ICRUDService<Model.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiInsertRequest>, TerminiService/*BaseCRUDService<Model.Termini, TerminiSearchRequest, Database.Termini, TerminiInsertRequest, TerminiInsertRequest>*/ >();
            services.AddScoped<IService<Model.Uloge, object>, UlogeService>();
            //services.AddScoped<ICRUDService<Model.RezervacijeGluveSobe, object, Model.RezervacijeGluveSobe, Model.RezervacijeGluveSobe>, BaseCRUDService<Model.RezervacijeGluveSobe, object, Database.RezervacijeGluveSobe, Model.RezervacijeGluveSobe, Model.RezervacijeGluveSobe>>();
            services.AddScoped<ICRUDService<Model.RezervacijeGluveSobe, RezervacijaGluveSobeSearchRequest, Model.RezervacijeGluveSobe, Model.RezervacijeGluveSobe>, RezervacijaGluveSobeService>();
            services.AddScoped<ICRUDService<Model.Ocjene, object, OcjenaInsertRequest, OcjenaInsertRequest>, BaseCRUDService<Model.Ocjene, object, Models.Ocjene, OcjenaInsertRequest, OcjenaInsertRequest>>();
            services.AddScoped<ISistemPreporuke, SistemPreporukeService>();

            var connection = Configuration.GetConnectionString("eMusicStudio");
            services.AddDbContext<_150192V1Context>(x => x.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
