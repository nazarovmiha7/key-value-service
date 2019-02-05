using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyValueService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;


namespace KeyValueService
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
            services.AddMvc();

            // Configure service in in-memory or persistance mode.
            var persistance = Configuration.GetValue<bool>("persistance");
            if (persistance)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("KeyValueService"));
                });

                services.AddScoped<IKeyValueService, PersistanceKeyValueService>();
                //services.AddScoped<ApplicationDbContext>();
            }
            else
                services.AddSingleton<IKeyValueService, InMemoryKeyValueService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
