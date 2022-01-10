using Sample.Models;
using Sample.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _service;

        public StationsController(IStationService service)
        {
            _service = service;
        }


        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
            var result = await _service.ReadStations();
            return result.Stations;
        }

        // GET: api/Stations/5

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Station>> GetStation(Guid id)
        //{
        //    var station = await _repo.GetStation(id);

        //    if (station == null)
        //    {
        //        return NotFound();
        //    }
        //    return station;
        //}


        [HttpPost]
        public async Task<ActionResult<Station>> GetByStation(Station station)
        {

            var list = new StationList() {
                Id = Guid.NewGuid(),
                Stations = (await _service.ReadStations()).Stations,
                CacheExpiry = DateTime.Now.AddMinutes(3)
            };

            return await _service.CompareStations(list,station,0);
        }


        [HttpGet("seed")]
        public async Task<ActionResult<bool>> Seed()
        {
            return await _service.FillStations();
        }

    }
}
