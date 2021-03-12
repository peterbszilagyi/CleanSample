using CleanSample.App.Common.Interface;
using CleanSample.Infra.Repository.EF;
using CleanSample.Test.Helpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CleanSample.Test.Infra.Repo
{
    [TestClass]
    public class AirplaneRepoTest: UnitTestBase
    {

        private static void SetupCleanDB(ServiceProvider sp)
        {
            var context = sp.GetService<CleanSampleContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void AbolishDB(ServiceProvider sp)
        {
            var context = sp.GetService<CleanSampleContext>();
            context.Database.EnsureDeleted();
        }


        [TestMethod]
        [DataRow("test airplane")]
        public async Task AddAirplane_ShouldWork(string airplaneName)
        {
            var sp = SetupDependencies(withDbContext: true);
            SetupCleanDB(sp);

            var repo = sp.GetService<IAirplaneRepository>();
            var result = await repo.AddAirplane(airplaneName);

            result.Id.Should().NotBe(0);
            result.Name.Should().Be(airplaneName);


            AbolishDB(sp);
        }

        [TestMethod]
        [DataRow("old name", "new name")]
        public async Task ModifyAirplane_ShouldWork(string oldName, string newName)
        {
            var sp = SetupDependencies(withDbContext: true);
            SetupCleanDB(sp);

            var repo = sp.GetService<IAirplaneRepository>();
            var originalAirplane = await repo.AddAirplane(oldName);
            var airplaneId = originalAirplane.Id;
            originalAirplane.Name.Should().Be(oldName);

            await repo.ModifyAirplane(airplaneId, newName);

            var modifiedAirplane = await repo.GetAirplane(airplaneId);
            modifiedAirplane.Name.Should().Be(newName);


            AbolishDB(sp);
        }

    }
}
