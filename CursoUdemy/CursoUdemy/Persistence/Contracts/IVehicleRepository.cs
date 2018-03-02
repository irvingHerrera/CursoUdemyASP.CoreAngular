using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoUdemy.Persistence.Contracts
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);

        Task<IEnumerable<Vehicle>> GetVehicles();

        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);
    }
}
