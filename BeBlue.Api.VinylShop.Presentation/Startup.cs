using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.ExternalServices;
using BeBlue.Api.VinylShop.LogicLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace BeBlue.Api.VinylShop.Presentation
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
			var mongoSettings = this.Configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
			var spotifySettings = this.Configuration.GetSection(nameof(SpotifySettings)).Get<SpotifySettings>();

			services.AddSingleton(mongoSettings);
			services.AddSingleton(spotifySettings);

			services.AddSingleton<HttpClient>();
			services.AddSingleton<ISpotifyClient, SpotifyClient>();
			services.AddSingleton<IMongoContext, MongoContext>();
			services.AddSingleton<IUnitOfWork, UnitOfWork>();
			services.AddSingleton<ICashbackCalculator, CashbackCalculator>();

			services.AddSingleton<CatalogBootstrapper>();
			services.AddSingleton<CashbackSettingsBootstrapper>();


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

			app.ApplicationServices.GetRequiredService<CatalogBootstrapper>();
			app.ApplicationServices.GetRequiredService<CashbackSettingsBootstrapper>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
