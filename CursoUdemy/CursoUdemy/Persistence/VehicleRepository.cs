using CursoUdemy.Models;
using CursoUdemy.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoUdemy.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext appDbContext;

        public VehicleRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated)
        {

            if (!includeRelated)
                return await appDbContext.Vehicle.FindAsync(id);

            return await appDbContext.Vehicle
                               .Include(v => v.Features)
                               .ThenInclude(vf => vf.Feature)
                               .Include(v => v.Model)
                               .ThenInclude(vf => vf.Make)
                               .SingleOrDefaultAsync(v => v.Id == id);
        }


        public void Add(Vehicle vehicle)
        {
            appDbContext.Vehicle.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            appDbContext.Remove(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles() =>
            await appDbContext.Vehicle
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .ToListAsync();
    }
}
