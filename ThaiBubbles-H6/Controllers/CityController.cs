using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private ICityRepositories _cityRepo;
        public CityController(ICityRepositories temp)
        {
            _cityRepo = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetCities()
        {
            try
            {
                var cities = await _cityRepo.GetAllCities();

                if (cities == null)
                {
                    return Problem("Nothing was returned from cities, this is unexpected");
                }
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{cityId}")]
        public async Task<ActionResult> GetCitiesById(int cityId)
        {
            try
            {
                var city = await _cityRepo.GetCityById(cityId);

                if (city == null)
                {
                    return NotFound($"City with id {cityId} was not found");
                }
                return Ok(city);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{cityId}")]
        public async Task<ActionResult> PutCity(int cityId, City city)
        {
            try
            {
                // Call the repository method to update the city
                var cityResult = await _cityRepo.UpdateCity(city, cityId);

                // If the result is null, return a NotFound response
                if (cityResult == null)
                {
                    return NotFound($"City with id {cityId} was not found");
                }

                // Return the updated city with a success response
                return Ok(cityResult);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a Problem response
                return Problem(ex.Message);
            }
        }

        //Create Method
        [HttpPost]
        public async Task<ActionResult> PostCity(City city)
        {
            try
            {
                var createCity = await _cityRepo.CreateCity(city);

                if (createCity == null)
                {
                    return StatusCode(500, "City was not created. Something failed...");
                }
                return CreatedAtAction("PostCity", new { cityId = createCity.CityID }, createCity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the city {ex.Message}");
            }
        }

        //Delete Method
        [HttpDelete("{cityId}")]
        public async Task<ActionResult> DeleteCity(int cityId)
        {
            try
            {
                var city = await _cityRepo.DeleteCity(cityId);

                if (city == null)
                {
                    return NotFound($"City with id {cityId} was not found");
                }
                return Ok(city);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

