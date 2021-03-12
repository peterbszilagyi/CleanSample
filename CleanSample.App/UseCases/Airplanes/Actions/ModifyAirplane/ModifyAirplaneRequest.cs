using Ardalis.GuardClauses;
using MediatR;

namespace CleanSample.App.UseCases.Airplanes.Actions.ModifyAirplane
{
    public class ModifyAirplaneRequest: IRequest
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ModifyAirplaneRequest(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}