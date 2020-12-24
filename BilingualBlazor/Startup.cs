using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using BilingualBlazor.Utils;

namespace BilingualBlazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<StateContainer>();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedUiCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en"),
                    new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedUiCultures;
                options.SupportedUICultures = supportedUiCultures;

                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
                {
                    var segments = context.Request.Path.Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    var currentCulture = "en";

                    if (segments.Length >= 1 && segments[0].Length == 2)
                    {
                        currentCulture = segments[0].Equals("en", StringComparison.OrdinalIgnoreCase) ? "en" : "fr";
                    }

                    var requestCulture = new ProviderCultureResult(currentCulture, currentCulture);

                    return await Task.FromResult(requestCulture);
                }));
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            var supportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("fr") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedUICultures = supportedCultures;
            });
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}