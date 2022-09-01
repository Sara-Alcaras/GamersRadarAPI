using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GamersRadarAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "GamersRadarAPI", 
                    Version = "v1",
                    Description = "API desenvolvida durante o programa Entry Point BRQ!",
                    TermsOfService = new Uri("https://GamersRadar.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sara Alcaras",
                        Url = new Uri("https://github.com/Sara-Alcaras")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "EduSync",
                        Url = new Uri("https://edusync.twygoead.com/dashboard_students")
                    }
                });

                // Adicionar configurações extras da documentação, para ler os XMLs
                var xmlArquivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                
                // Combina os arquivos gerados
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlArquivo));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamersRadarAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")
                ),
                RequestPath = "/StaticFiles"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
