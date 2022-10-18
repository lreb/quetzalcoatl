using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quetzalcoatl.Infrastructure.Mediatr;
using Quetzalcoatl.Infrastructure.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace Quetzalcoatl.Api
{
    /// <summary>
    /// Application startup instructions
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Application setup
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Application inyection
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerService(Configuration);
            services.AddMediatR(MediatrUtility.GetMediatrAssembliesToScan("Quetzalcoatl.BusinessLogic"));
            services.AddControllers();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.EnableSwaggerPipeline(Configuration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        //private Type[] GetMediatrAssembliesToScan()
        //{
        //    var assemblies = Assembly.Load("Quetzalcoatl.BusinessLogic")
        //        .GetTypes()
        //        .ToArray();

        //    return assemblies;
        //}
    }
}
