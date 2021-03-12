using CleanSample.App.Common.Interface;
using CleanSample.App.UseCases.Airplanes.Actions;
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

namespace CleanSample.Test
{
    [TestClass]
    public class AddAirplaneTest : UnitTestBase
    {
        [TestMethod]
        [DataRow("test airplane")]
        public async Task AddAirplane_ShouldWork(string name)
        {
            List<Airplane> airplanes = new List<Airplane>();
            var sp = SetupDependencies(services =>
            {
                var dateTimeProvider = new Mock<IDateTimeProvider>();
                var airplaneRepo = new Mock<IAirplaneRepository>();
                airplaneRepo.Setup(a => a.AddAirplane(name)).Callback<string>(airplaneName =>
                {
                    var airplane = new Airplane() { Id = 1, Name = airplaneName };
                    airplanes.Add(airplane);
                });
                services.AddSingleton(airplaneRepo.Object);
            });

            var mediator = sp.GetService<IMediator>();

            var addAirplaneRequest = new AddAirplaneRequest(name);
            await mediator.Send(addAirplaneRequest);

            airplanes.Count.Should().Be(1);

        }

        [TestMethod]
        [DataRow("")]
        public async Task AddAirplane_EmptyName(string name)
        {
            List<Airplane> airplanes = new List<Airplane>();
            var sp = SetupDependencies(services =>
            {
                var dateTimeProvider = new Mock<IDateTimeProvider>();
                var airplaneRepo = new Mock<IAirplaneRepository>();
                airplaneRepo.Setup(a => a.AddAirplane(name)).Callback<string>(airplaneName =>
                {
                    var airplane = new Airplane() { Id = 1, Name = airplaneName };
                    airplanes.Add(airplane);
                });
                services.AddSingleton(airplaneRepo.Object);
            });

            var mediator = sp.GetService<IMediator>();

            var addAirplaneRequest = new AddAirplaneRequest(name);

            Func<Task> call = async () => await mediator.Send(addAirplaneRequest);
            await call.Should().ThrowAsync<ArgumentException>();
        }

        [TestMethod]
        [DataRow(null)]
        public async Task AddAirplane_NameIsNull(string name)
        {
            List<Airplane> airplanes = new List<Airplane>();
            var sp = SetupDependencies(services =>
            {
                var dateTimeProvider = new Mock<IDateTimeProvider>();
                var airplaneRepo = new Mock<IAirplaneRepository>();
                airplaneRepo.Setup(a => a.AddAirplane(name)).Callback<string>(airplaneName =>
                {
                    var airplane = new Airplane() { Id = 1, Name = airplaneName };
                    airplanes.Add(airplane);
                });
                services.AddSingleton(airplaneRepo.Object);
            });

            var mediator = sp.GetService<IMediator>();

            var addAirplaneRequest = new AddAirplaneRequest(null);

            Func<Task> call = async () => await mediator.Send(addAirplaneRequest);
            await call.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
