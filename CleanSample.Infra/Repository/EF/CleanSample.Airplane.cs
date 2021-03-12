using CleanSample.App.Common.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CleanSample.Domain.Entities;

namespace CleanSample.Infra.Repository.EF
{
    public partial class CleanSampleContext: IAirplaneRepository
    {
        public async Task<Airplane> AddAirplane(string name)
        {
            var airplane = new Airplane() { Name = name };
            Airplanes.Add(airplane);
            await SaveChangesAsync();
            return airplane;
        }

        public async Task ModifyAirplane(int id, string name)
        {
            var airplane = await Airplanes.FindAsync(id);
            airplane.Name = name;
            await SaveChangesAsync();
        }

        public async Task DeleteAirplane(int id)
        {
            Airplanes.Remove(await Airplanes.FindAsync(id));
            SaveChanges();
        }

        public async Task<Airplane> GetAirplane(int id)
        {         
            return await Airplanes.FindAsync(id);
        }

        public async Task<IEnumerable<Airplane>> GetAllAirplanes()
        {
            return Airplanes.ToList();
        }
    }
}
