using CursoUdemy.Extensions;
using CursoUdemy.Models;
using CursoUdemy.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();

            var query = appDbContext.Vehicle
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .AsQueryable();

            var dictionary = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                { "make", v => v.Model.Make.Name },
                { "model", v => v.Model.Name },
                { "contactName", v => v.ContactName },
                { "id", v => v.Id }
            };

            query = query.ApplyOrdering(queryObj, dictionary);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
            
    }
}
