using Ardalis.GuardClauses;
using CleanSample.App.Common.BaseClasses;
using CleanSample.App.Common.Constants;
using CleanSample.App.Common.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSample.App.UseCases.Airplanes.Actions.ModifyAirplane
{
    public class ModifyAirplane: ActionCommand<ModifyAirplaneRequest>
    {
        private readonly IAirplaneRepository airplaneRepository;
        private readonly ILogger<ModifyAirplane> logger;

        public ModifyAirplane(ILogger<ModifyAirplane> logger, IAirplaneRepository airplaneRepository)
            : base(logger)
        {
            this.logger = logger;
            this.airplaneRepository = airplaneRepository;
        }

        public override async Task CheckPreconditions(ModifyAirplaneRequest request)
        {
            await base.CheckPreconditions(request);

            Guard.Against.NegativeOrZero(request.Id, nameof(request.Id));
            Guard.Against.NullOrWhiteSpace(request.Name, nameof(request.Name));

            var airplane = await airplaneRepository.GetAirplane(request.Id);
            Guard.Against.Null(airplane, nameof(airplane));
        }

        protected override async Task Authorize(ModifyAirplaneRequest request)
        {
            //TODO: write authorization logic
            bool isAuthorized = true;

            if (!isAuthorized)
            {

                logger.LogWarning($"{LoggingContants.SECU} - Authorization error in {nameof(ModifyAirplane)}. Request: {request}");
                throw new UnauthorizedAccessException("ERROR TEXT FROM RESOURCE");
            }
        }

        protected override async Task Execute(ModifyAirplaneRequest request)
        {
            await airplaneRepository.ModifyAirplane(request.Id, request.Name);
        }
    }
}
