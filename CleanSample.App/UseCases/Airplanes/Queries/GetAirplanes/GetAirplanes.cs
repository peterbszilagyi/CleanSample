using CleanSample.App.Common.BaseClasses;
using CleanSample.App.Common.Interface;
using CleanSample.App.UseCase.Queries.Airplane;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSample.App.UseCases.Airplanes.Queries.GetAirplanes
{
    public class GetAirplanes : QueryCommand<GetAirplanesRequest, GetAirplanesRespone>
    {
        private readonly IAirplaneRepository airplaneRepository;
        private readonly ILogger<GetAirplanes> logger;
        public GetAirplanes(ILogger<GetAirplanes> logger, IAirplaneRepository airplaneRepository)
            : base(logger)
        {
            this.logger = logger;
            this.airplaneRepository = airplaneRepository;
        }

        protected override async Task Authorize(GetAirplanesRequest request)
        {
            //TODO: write authorization logic
            bool isAuthorized = true;

            if (!isAuthorized)
            {
                logger.LogWarning($"[$SECURITY$] - Authorization error in {nameof(GetAirplanes)}. Request: {request}");
                throw new UnauthorizedAccessException("RESOURCE ERROR");
            }
        }

        protected override async Task<GetAirplanesRespone> Execute(GetAirplanesRequest request)
        {
            var airplanes = await airplaneRepository.GetAllAirplanes();
            List<AirplaneDTO> result = airplanes.Select(a => new AirplaneDTO()
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();

            var response = new GetAirplanesRespone()
            {
                Airplanes = result
            };

            return response;
        }


    }
}
