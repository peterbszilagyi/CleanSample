using CleanSample.App.Common.Interface;
using CleanSample.App.UseCase.Airplanes.Queries.GetAirplane;
using CleanSample.Infra.Repository.EF;
using CleanSample.Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using System;
using System.Configuration;

namespace CleanSample.Test.Helpers
{
    [TestClass]
    public class UnitTestBase
    {
        private IConfiguration Configuration;
        public ServiceProvider SetupDependencies(Action<ServiceCollection> moqsToInject = null, bool withDbContext = false)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            var services = new ServiceCollection();

            services.AddTransient<IDateTimeProvider, SystemDateTimeProvider>();
            //no other handlers should be registered
            services.AddMediatR(typeof(GetAirplane));

            if (withDbContext)
            {
                //EF ---->
                services.AddDbContext<CleanSampleContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("CleanSampleTestConnection"),
                        b => b.MigrationsAssembly(typeof(CleanSampleContext).Assembly.FullName)));

                services.AddScoped<IAirplaneRepository>(provider => provider.GetService<CleanSampleContext>());
                //<---- EF
            }

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .WriteTo.Debug(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {ThreadId} {Level:u3} {Scope} [{SourceContext:l}] {Message}{NewLine}{Exception}")
              .Enrich.With<ThreadIdEnricher>()
              .CreateLogger();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            moqsToInject?.Invoke(services); // inject moq registrations

            return services.BuildServiceProvider();
        }
    }
}
