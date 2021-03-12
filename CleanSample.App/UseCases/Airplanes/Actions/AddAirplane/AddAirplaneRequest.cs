using MediatR;

namespace CleanSample.App.UseCases.Airplanes.Actions
{
    public sealed class AddAirplaneRequest: IRequest
    {
        public string Name { get; init; }
        public AddAirplaneRequest(string name)
        {
            Name = name;
        }
    }
}