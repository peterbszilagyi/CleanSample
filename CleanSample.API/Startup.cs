using CleanSample.App.Common.Interface;
using CleanSample.App.UseCase.Airplanes.Queries.GetAirplane;
using CleanSample.Infra.Repository.EF;
using CleanSample.Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers;
using System;

namespace CleanSample.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .Enrich.FromLogContext()
               .Enrich.With<ThreadIdEnricher>()
               .CreateLogger();

            Log.Information("Logger init done");

            AppDomain.CurrentDomain.DomainUnload += (o, e) => Log.CloseAndFlush();
            AppDomain.CurrentDomain.UnhandledException += (o, e) => Log.Error(e.ExceptionObject as Exception, "UnhandledException");

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IDateTimeProvider, SystemDateTimeProvider>();

            //no other handlers should be registered
            services.AddMediatR(typeof(GetAirplane));


            //EF ---->
            services.AddDbContext<CleanSampleContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CleanSampleConnection"),
                    b => b.MigrationsAssembly(typeof(CleanSampleContext).Assembly.FullName)));

            services.AddScoped<IAirplaneRepository>(provider => provider.GetService<CleanSampleContext>());

            //<---- EF

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            loggerFactory.AddSerilog();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
