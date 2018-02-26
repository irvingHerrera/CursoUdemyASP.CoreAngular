using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CursoUdemy.Models;
using CursoUdemy.Persistence;
using CursoUdemy.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CursoUdemy.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly AppDbContext appDbContext;

        public VehiclesController(IMapper mapper, AppDbContext appDbContext)
        {
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleAsync(int id, [FromBody]SaveVehicleResource vehicle)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objVehicle = await appDbContext.Vehicle
                                .Include(v => v.Features)
                                .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                .ThenInclude(vf => vf.Make)
                                .SingleOrDefaultAsync(v => v.Id == id);

            if (objVehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicle, objVehicle);
            objVehicle.LastUpdate = DateTime.Now;

            await appDbContext.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(objVehicle);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleAsync([FromBody]SaveVehicleResource vehicle)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var objVehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicle);

            objVehicle.LastUpdate = DateTime.Now;
            appDbContext.Vehicle.Add(objVehicle);
            await appDbContext.SaveChangesAsync();

            objVehicle = await appDbContext.Vehicle
                                .Include(v => v.Features)
                                .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                .ThenInclude(vf => vf.Make)
                                .SingleOrDefaultAsync(v => v.Id == objVehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(objVehicle);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await appDbContext.Vehicle.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            appDbContext.Remove(vehicle);
            await appDbContext.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await appDbContext.Vehicle
                                .Include(v => v.Features)
                                .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                .ThenInclude(vf => vf.Make)
                                .SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}