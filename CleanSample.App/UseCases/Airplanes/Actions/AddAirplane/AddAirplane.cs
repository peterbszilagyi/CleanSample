using Ardalis.GuardClauses;
using CleanSample.App.Common.BaseClasses;
using CleanSample.App.Common.Constants;
using CleanSample.App.Common.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CleanSample.App.UseCases.Airplanes.Actions
{
    public class AddAirplane : ActionCommand<AddAirplaneRequest>
    {
        private readonly IAirplaneRepository airplaneRepository;
        private readonly ILogger<AddAirplane> logger;

        public AddAirplane(ILogger<AddAirplane> logger, IAirplaneRepository airplaneRepository)
            : base(logger)
        {
            this.logger = logger;
            this.airplaneRepository = airplaneRepository;
        }

        public override async Task CheckPreconditions(AddAirplaneRequest request)
        {
            await base.CheckPreconditions(request);
            Guard.Against.NullOrWhiteSpace(request.Name, nameof(request.Name));            
        }

        protected override async Task Authorize(AddAirplaneRequest request)
        {
            //TODO: write authorization logic
            bool isAuthorized = true;

            if (!isAuthorized)
            {

                logger.LogWarning($"{LoggingContants.SECU} - Authorization error in {nameof(AddAirplane)}. Request: {request}");
                throw new UnauthorizedAccessException("ERROR TEXT FROM RESOURCE");
            }
        }

        protected override async Task Execute(AddAirplaneRequest request)
        {
            await airplaneRepository.AddAirplane(request.Name);
        }
    }
}
