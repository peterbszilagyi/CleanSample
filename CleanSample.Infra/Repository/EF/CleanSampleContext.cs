using CleanSample.App.Common.Interface;
using CleanSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanSample.Infra.Repository.EF
{
    public partial class CleanSampleContext : DbContext
    {
        private readonly ILogger<CleanSampleContext> logger;
        private readonly IDateTimeProvider dateTimeProvider;
        public DbSet<Airplane> Airplanes { get; set; }


        public CleanSampleContext(ILogger<CleanSampleContext> logger,
                                      IDateTimeProvider dateTimeProvider,
                                      DbContextOptions options) 
            : base(options)
        {
            this.logger = logger;
            this.dateTimeProvider = dateTimeProvider;
        }
    }
}
