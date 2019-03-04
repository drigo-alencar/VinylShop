using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BeBlue.Api.VinylShop.ExternalServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BeBlue.Api.VinylShop.DataLayer;

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
			services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

			var mongoSettings = this.Configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
			var spotifySettings = this.Configuration.GetSection(nameof(SpotifySettings)).Get<SpotifySettings>();

			services.AddSingleton(mongoSettings);
			services.AddSingleton(spotifySettings);

			services.AddSingleton<HttpClient>();
			services.AddSingleton<ISpotifyClient, SpotifyClient>();
			services.AddSingleton<IMongoContext, MongoContext>();
			services.AddSingleton<IUnitOfWork, UnitOfWork>();

			services.AddSingleton<CatalogBootStrapper>();


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

			app.ApplicationServices.GetRequiredService<CatalogBootStrapper>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
