using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Quetzalcoatl.Infrastructure.Swagger
{
    /// <summary>
    /// Swagger extension
    /// </summary>
    public static class SwaggerExtension
    {
		//public static void AddSwaggerService(this IServiceCollection services)
		//{
		//    services.AddSwaggerGen(c =>
		//    {
		//        c.SwaggerDoc("v1", new OpenApiInfo { Title = "quetzalcoatl.api", Version = "v1" });
		//    });
		//}

		#region Swagger configurationuration
		/// <summary>
		/// Method to configurationure the Swagger Services within the Application services interface
		/// </summary>
		/// <param name="services">The Service Collection <see cref="IServiceCollection"/></param>
		/// <param name="configuration">The Service Collection <see cref="IConfiguration"/></param>
		public static void AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = configuration["SwaggerOptions:Title"],
					Version = configuration["SwaggerOptions:Version"],
					Description = configuration["SwaggerOptions:Description"],
					TermsOfService = new Uri(configuration["SwaggerOptions:TermsOfService"]), 
					Contact = new OpenApiContact { 
						Email = configuration["SwaggerOptions:ContactEmail"],
						Name = configuration["SwaggerOptions:ContactEmail"],
						Url = new Uri(configuration["SwaggerOptions:TermsOfService"])
					},
					License = new OpenApiLicense {
						Name = configuration["SwaggerOptions:LicenseName"],
						Url = new Uri(configuration["SwaggerOptions:LicenseUrl"])
					}
				});
				
				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{configuration["SwaggerOptions:DocumentationFileName"]}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
		}
		#endregion

		/// <summary>
		/// Enable Swagger pipeline
		/// </summary>
		/// <param name="app">application configurationuration <see cref="IApplicationBuilder"/></param>
		/// <param name="configuration">application settings <see cref="Iconfigurationuration"/></param>
		public static void EnableSwaggerPipeline(this IApplicationBuilder app, IConfiguration configuration)
		{
			app.UseSwagger();
			app.UseSwaggerUI(option => 
			{
				option.SwaggerEndpoint(
					configuration["SwaggerOptions:SwaggerJSONEndpoints"],
					$"{configuration["SwaggerOptions:Title"]} {configuration["SwaggerOptions:Version"]}");
				//option.RoutePrefix = string.Empty;
			});
		}
	}
}
