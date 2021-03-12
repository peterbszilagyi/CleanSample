using Ardalis.GuardClauses;
using MediatR;

namespace CleanSample.App.UseCases.Airplanes.Actions.DeleteAirplane
{
    public class DeleteAirplaneRequest: IRequest
    {
        public int Id { get; init; }

        public DeleteAirplaneRequest(int id)
        {
            Id = id;
        }
    }
}