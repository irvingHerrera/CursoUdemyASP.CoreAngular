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
using CursoUdemy.Persistence.Contracts;

namespace CursoUdemy.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, 
                                IVehicleRepository repository,
                                IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleAsync(int id, [FromBody]SaveVehicleResource vehicle)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objVehicle = await repository.GetVehicle(id);

            if (objVehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicle, objVehicle);
            objVehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            objVehicle = await repository.GetVehicle(objVehicle.Id);

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
            repository.Add(objVehicle);
            await unitOfWork.CompleteAsync();

            objVehicle = await repository.GetVehicle(objVehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(objVehicle);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}