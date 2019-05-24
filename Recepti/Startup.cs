using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recepti.AttributeAdapters;
using Recepti.Context;
using Recepti.Filters;
using Recepti.Repository.AuditRepo;
using Recepti.Repository.ErrorLoggingRepo;
using Recepti.Repository.KorisnikRepo;
using Recepti.Repository.ReceptRepo;

namespace Recepti
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                // Uncomment for XSS vulnerability
                //options.Cookie.HttpOnly = false;
            });

            services.AddMvc(options =>
            {
                bool.TryParse(Configuration["AuditingEnabled"], out bool auditingEnabled);

                if (auditingEnabled)
                {
                    options.Filters.Add(typeof(AuditFilter));
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddSessionStateTempDataProvider();

            var connString = Configuration["ReceptiConnectionString"];
            services.AddDbContext<EFContext>(options => options.UseSqlServer(connString));
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            // Singleton injection
            services.AddSingleton<IValidationAttributeAdapterProvider, AttributeAdapterProvider>();

            // Repository injection
            services.AddScoped<IKorisnikRepo, KorisnikRepo>();
            services.AddScoped<IReceptRepo, ReceptRepo>();
            services.AddScoped<IErrorLoggingRepo, ErrorLoggingRepo>();
            services.AddScoped<IAuditRepo, AuditRepo>();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
