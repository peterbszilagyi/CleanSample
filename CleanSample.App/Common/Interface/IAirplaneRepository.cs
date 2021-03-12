using CleanSample.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanSample.App.Common.Interface
{
    public interface IAirplaneRepository
    {
        Task<Airplane> GetAirplane(int id);

        Task<IEnumerable<Airplane>> GetAllAirplanes();

        Task<Airplane> AddAirplane(string name);

        Task DeleteAirplane(int id);
        Task ModifyAirplane(int id, string name);
    }
}
