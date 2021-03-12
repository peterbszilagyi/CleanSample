using CleanSample.App.Common.BaseClasses;
using CleanSample.App.UseCase.Queries.Airplane;

namespace CleanSample.App.UseCase.Airplanes.Queries.GetAirplane
{
    public class GetAirplaneResponse: ResponseModel
    {
        public AirplaneDTO Airplane { get; set; }
    }
}
