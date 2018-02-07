using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CursoUdemy.Models;
using CursoUdemy.Persistence;
using CursoUdemy.Resources;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> CreateVehicleAsync([FromBody]VehicleResource vehicle)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objVehicle = mapper.Map<VehicleResource, Vehicle>(vehicle);
            objVehicle.LastUpdate = DateTime.Now;
            appDbContext.Vehicle.Add(objVehicle);
            await appDbContext.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(objVehicle);

            return Ok(result);
        }
    }
}