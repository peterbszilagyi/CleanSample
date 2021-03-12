using Ardalis.GuardClauses;
using CleanSample.App.Common.BaseClasses;
using CleanSample.App.Common.Constants;
using CleanSample.App.Common.Interface;
using CleanSample.App.UseCase.Queries.Airplane;
using CleanSample.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CleanSample.App.UseCase.Airplanes.Queries.GetAirplane
{
    public class GetAirplane : QueryCommand<GetAirplaneRequest, GetAirplaneResponse>
    {
        private readonly IAirplaneRepository airplaneRepository;
        private readonly ILogger<GetAirplane> logger;
        
        public GetAirplane(ILogger<GetAirplane> logger, IAirplaneRepository airplaneRepository)
            :base(logger)
        {
            this.logger = logger;
            this.airplaneRepository = airplaneRepository;
        }

        protected override async Task Authorize(GetAirplaneRequest request)
        {
            //TODO: write authorization logic
            bool isAuthorized = true;

            if (!isAuthorized)
            {                
                logger.LogWarning($"{LoggingContants.SECU} - Authorization error in {nameof(GetAirplane)}. Request: {request}");
                throw new UnauthorizedAccessException("ERROR TEXT FROM RESOURCE");
            }
        }

        protected override async Task<GetAirplaneResponse> Execute(GetAirplaneRequest request)
        {
            Airplane airplane = await airplaneRepository.GetAirplane(request.Id);
            Guard.Against.Null(airplane, nameof(airplane));
            var response = new GetAirplaneResponse()
            {
                Airplane = new AirplaneDTO()
                {
                    Id = airplane.Id,
                    Name = airplane.Name
                }
            };
            return response;
        }
    }
}
