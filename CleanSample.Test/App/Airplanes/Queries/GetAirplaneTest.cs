using CleanSample.App.Common.Interface;
using CleanSample.App.UseCase.Airplanes.Queries.GetAirplane;
using CleanSample.Domain.Entities;
using CleanSample.Test.Helpers;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanSample.Test.App.Airplanes.Queries
{
    [TestClass]
    public class GetAirplaneTest: UnitTestBase
    {
        [TestMethod]
        [DataRow(1, "airplane 1")]        
        public async Task GetAirplane_ShouldWork(int id, string name)
        {
            List<Airplane> airplanes = new List<Airplane>
            {
                new Airplane(){ Id = id, Name = name},
                new Airplane(){ Id = 100, Name = "some airplane"},
                new Airplane(){ Id = 200, Name = "some airplane"}
            };

            Airplane airplane = null;

            var sp = SetupDependencies(services => {
                var dateTimeProvider = new Mock<IDateTimeProvider>();
                var airplaneRepo = new Mock<IAirplaneRepository>();
                airplaneRepo.Setup(a => a.GetAirplane(id)).Callback<int>(id => airplane = airplanes.Find(a => a.Id == id)).ReturnsAsync(() => airplane);
                services.AddSingleton(airplaneRepo.Object);
            });

            var mediator = sp.GetService<IMediator>();
            var result = await mediator.Send(new GetAirplaneRequest(id));
            result.Airplane.Id.Should().Be(id);
            result.Airplane.Name.Should().Be(name);
        }


        [TestMethod]
        [DataRow(1, "airplane 1")]
        public async Task GetAirplane_DoesntExist(int id, string name)
        {
            List<Airplane> airplanes = new List<Airplane>
            {                
                new Airplane(){ Id = 100, Name = "some airplane"},
                new Airplane(){ Id = 200, Name = "some airplane"}
            };

            Airplane airplane = null;

            var sp = SetupDependencies(services => {
                var dateTimeProvider = new Mock<IDateTimeProvider>();
                var airplaneRepo = new Mock<IAirplaneRepository>();
                airplaneRepo.Setup(a => a.GetAirplane(id)).Callback<int>(id => airplane = airplanes.Find(a => a.Id == id)).ReturnsAsync(() => airplane);
                services.AddSingleton(airplaneRepo.Object);
            });

            var mediator = sp.GetService<IMediator>();
            Func<Task> call = async () => await mediator.Send(new GetAirplaneRequest(id));
            await call.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
