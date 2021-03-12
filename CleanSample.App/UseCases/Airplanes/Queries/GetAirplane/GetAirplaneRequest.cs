using MediatR;

namespace CleanSample.App.UseCase.Airplanes.Queries.GetAirplane
{
    public class GetAirplaneRequest: IRequest<GetAirplaneResponse>
    {
        public int Id { get; init; }

        public GetAirplaneRequest(int id)
        {
            Id = id;
        }
    }
}
