using CanWeFixIt.Core.Interfaces;
using CanWeFixIt.Service.Data;
using CanWeFixIt.Service.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CanWeFixIt.Api
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
            // TODO: add logger
            services.AddAutoMapper(typeof(InstrumentMappingProfile).Assembly);

            var dbConnectionString = Configuration.GetValue<string>("DbSettings:CanWeFixItDBConnectionString");
            var dbRetryTimes = Configuration.GetValue<int>("DbSettings:CanWeFixItDbMaxRetryTimes");
            var dbMaxTimeout = Configuration.GetValue<int>("DbSettings:CanWeFixItDbMaxTimeout");

            services.AddDbContext<CanWeFixItContext>(opt => opt.UseSqlite(  // TODO: retry
                    dbConnectionString,
                    sqlOptions => sqlOptions
                        .CommandTimeout(dbMaxTimeout)), 0);

            services.AddScoped<IInstrumentRepository, InstrumentRepository>();
            services.AddScoped<IMarketDataRepository, MarketDataRepository>();
            services.AddScoped<IInstrumentService, InstrumentService>();
            services.AddScoped<IMarketDataService, MarketDataService>();

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CanWeFixIt.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CanWeFixIt.Api v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}