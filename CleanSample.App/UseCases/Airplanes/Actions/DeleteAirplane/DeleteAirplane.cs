using Ardalis.GuardClauses;
using CleanSample.App.Common.BaseClasses;
using CleanSample.App.Common.Constants;
using CleanSample.App.Common.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CleanSample.App.UseCases.Airplanes.Actions.DeleteAirplane
{
    public class DeleteAirplane : ActionCommand<DeleteAirplaneRequest>
    {
        private readonly IAirplaneRepository airplaneRepository;
        private readonly ILogger<DeleteAirplane> logger;

        public DeleteAirplane(ILogger<DeleteAirplane> logger, IAirplaneRepository airplaneRepository)
            : base(logger)
        {
            this.logger = logger;
            this.airplaneRepository = airplaneRepository;
        }

        public override async Task CheckPreconditions(DeleteAirplaneRequest request)
        {
            await base.CheckPreconditions(request);            
            var airplaneToDelete = await airplaneRepository.GetAirplane(request.Id);
            Guard.Against.Null(airplaneToDelete, nameof(airplaneToDelete));            
        }

        protected override async Task Authorize(DeleteAirplaneRequest request)
        {
            //TODO: write authorization logic
            bool isAuthorized = true;

            if (!isAuthorized)
            {

                logger.LogWarning($"{LoggingContants.SECU} - Authorization error in {nameof(DeleteAirplane)}. Request: {request}");
                throw new UnauthorizedAccessException("ERROR TEXT FROM RESOURCE");
            }
        }

        protected override async Task Execute(DeleteAirplaneRequest request)
        {
            await airplaneRepository.DeleteAirplane(request.Id);
        }
    }
}
