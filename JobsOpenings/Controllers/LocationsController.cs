using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using JobsOpenings.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsOpenings.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsRepository _locationsRepository;
        public LocationsController(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLocations(Models.Location request)
        {
            try
            {
                Location response = new Location();
                Location data = DataMapper.location(request, response);
                bool success = _locationsRepository.AddLocation(data);
                if (success)
                {
                    // Manually construct the location URL
                    var locationUri = $"{Request?.Scheme}://{Request?.Host.ToUriComponent()}/api/v1/locations/{data.Id}";
                    return Created(locationUri, 201);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLocations(int id, Models.Location request)
        {
            try
            {
                bool success = _locationsRepository.UpdateLocations(id, request);
                if (success)
                {
                    return Ok();
                }
                else { return BadRequest("Location id not found"); }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<Models.Location>> GetLocations()
        {
            try
            {
                List<Location> locations = _locationsRepository.GetLocations();
                if (locations != null)
                {
                    return Ok(locations);
                }
                else { return BadRequest(); }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
