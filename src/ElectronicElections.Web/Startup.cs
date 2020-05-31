using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ElectronicElections.Data;
using ElectronicElections.Data.Managers;
using ElectronicElections.Infrastructure.Services;
using AutoMapper;
using System.Reflection;
using ElectronicElections.Infrastructure.Mapping;
using System.Collections.Generic;

namespace ElectronicElections.Web
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
            services.AddDbContext<ElectronicElectionsDbContext>(opts => 
            {
                opts.UseSqlServer(Configuration.GetConnectionString("ElectronicElectionsDb"));
                opts.UseLazyLoadingProxies();
            });
           
            services.AddScoped<ElectionsManager>();

            services.AddTransient<VoteService>();
            services.AddTransient<ElectionsService>();

            var autoMapperConfig = new AutoMapperConfig();

            var assembliesWithMappings = new List<Assembly>
            {
                Assembly.Load("ElectronicElections.Data"),
                Assembly.Load("ElectronicElections.Infrastructure"),
                Assembly.Load("ElectronicElections.Web")
            };

            autoMapperConfig.Execute(assembliesWithMappings);
            
            IMapper mapper = AutoMapperConfig.Configuration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
