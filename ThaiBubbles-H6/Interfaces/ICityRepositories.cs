using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Interfaces
{
    public interface ICityRepositories
    {
        public Task<List<City>> GetAllCities();
        public Task<City> GetCityById(int cityId);
        public Task<City> CreateCity(City city);
        public Task<City> UpdateCity(City city, int cityId);
        public Task<City> DeleteCity(int cityId);
    }
}
