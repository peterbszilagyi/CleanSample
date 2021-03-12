using CleanSample.App.UseCase.Airplanes.Queries.GetAirplane;
using CleanSample.App.UseCase.Queries.Airplane;
using CleanSample.App.UseCases.Airplanes.Actions;
using CleanSample.App.UseCases.Airplanes.Actions.DeleteAirplane;
using CleanSample.App.UseCases.Airplanes.Actions.ModifyAirplane;
using CleanSample.App.UseCases.Airplanes.Queries.GetAirplanes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanSample.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirplaneController : ControllerBase
    {
        private ISender mediator;
        private ILogger<AirplaneController> logger;

        public AirplaneController(ILogger<AirplaneController> logger, ISender mediator)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        //api/airplane/getall
        //f.e.:GET http://localhost:61993/api/airplane/getall
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<List<AirplaneDTO>>> GetAll()
        {
            var request = new GetAirplanesRequest();
            var result = await mediator.Send(request);
            return result.Airplanes;
        }

        //api/airplane/1
        //f.e.:GET http://localhost:61993/api/airplane/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AirplaneDTO>> GetAirplane(int id)
        {
            var request = new GetAirplaneRequest(id);
            var result = await mediator.Send(request);
            return result.Airplane;
        }

        //api/airplane
        //f.e.: PUT http://localhost:61993/api/airplane
        //in the request body with JSON type:
        //{
        //    "name": "New airplane"
        //}
        [HttpPut]        
        public async Task AddAirplane([FromBody] AirplaneDTO airplane)
        {
            var request = new AddAirplaneRequest(airplane.Name);
            await mediator.Send(request);
        }


        //api/airplane
        //f.e.: POST http://localhost:61993/api/airplane
        //in the request body with JSON type:
        //{
        //    "id": "4"
        //    "name": "Modified name"
        //}
        [HttpPost]        
        public async Task ModifyAirplane([FromBody] AirplaneDTO airplane)
        {
            var request = new ModifyAirplaneRequest(airplane.Id, airplane.Name);
            await mediator.Send(request);
        }


        //api/airplanes/1        
        //f.e.: DELETE http://localhost:61993/api/airplane/5        
        [HttpDelete("{id}")]
        public async Task DeleteAirplane(int id)
        {
            var request = new DeleteAirplaneRequest(id);
            await mediator.Send(request);
        }
    }
}
