using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using mongodb_dotnet_example.Models;
using mongodb_dotnet_example.Services;
using Serilog;

namespace mongodb_dotnet_example
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
            services.ConfigureOptions<GamesDatabaseSettingsOptions>();
            // configuring CORS options
            services.ConfigureOptions<CORSSettingsOptions>();
            IConfiguration configuration = Configuration; // Assuming you have IConfiguration injected into your class
           
            var corsOptions = Configuration.GetSection(CORSSettingsOptions.SectionName).Get<CORSOptions>();

            //setting up orgins for cofigured client urls 
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            corsOptions.AllowedHosts
                                .Select(o => o.Trim().RemovePostFix("/"))
                                .ToArray()
                        )
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders(); // Remove the default logging providers
                loggingBuilder.AddSerilog();     // Use Serilog as the logger
            });
            // adding Mongoclient service 
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<GamesDatabaseSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });
            services.AddTransient<IGameService,GamesService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mongodb_dotnet_example v1"));

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
