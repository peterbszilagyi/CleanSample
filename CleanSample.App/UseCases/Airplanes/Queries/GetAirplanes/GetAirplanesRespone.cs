using CleanSample.App.Common.BaseClasses;
using CleanSample.App.UseCase.Queries.Airplane;
using System.Collections.Generic;

namespace CleanSample.App.UseCases.Airplanes.Queries.GetAirplanes
{
    public sealed class GetAirplanesRespone : ResponseModel
    {       
        public List<AirplaneDTO> Airplanes { get; set; }
    }
}
