
namespace ThaiBubbles_H6.Repositories
{
    public class CityRepositories : ICityRepositories
    {
        //dependency injection
        private DatabaseContext _context { get; set; }
        public CityRepositories(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<City> CreateCity(City newCity)
        {
            _context.City.Add(newCity);
            await _context.SaveChangesAsync();
            return newCity;
        }

        public async Task<List<City>> GetAllCities()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<City> GetCityById(int cityId)
        {
            return await _context.City.FirstOrDefaultAsync(x => x.CityID == cityId);
        }

        public async Task<City> UpdateCity(City updatecity, int cityId)
        {
            City city = await GetCityById(cityId);
            if (city != null && UpdateCity != null)
            {
                city.CityID = updatecity.CityID;
                city.CityName = updatecity.CityName;
                city.ZIPCode = updatecity.ZIPCode;

                await _context.SaveChangesAsync();
            }
            return city;
        }
        public async Task<City> DeleteCity(int cityId)
        {
            City city = await GetCityById(cityId);
            if (city != null)
            {
                _context.City.Remove(city);
                await _context.SaveChangesAsync();
            }
            return city;
        }

    }
}
