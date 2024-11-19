using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Repositories
{
    public class CityRepositoriesTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private CityRepositories _cityRepository;

        public CityRepositoriesTests()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "CityRepositoryTests")
                .Options;

            _context = new DatabaseContext(_options);
            _cityRepository = new CityRepositories(_context);
        }

        [Fact]
        public async Task CreateCity_ShouldAddNewCity_WhenCityIsValid()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var newCity = new City { CityID = 1, CityName = "Test City", ZIPCode = 12345 };

            // Act
            var result = await _cityRepository.CreateCity(newCity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newCity.CityName, result.CityName);
            Assert.Equal(1, _context.City.Count());
        }

        [Fact]
        public async Task GetAllCities_ShouldReturnListOfCities_WhenCitiesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.City.Add(new City { CityID = 1, CityName = "City One", ZIPCode = 12345 });
            _context.City.Add(new City { CityID = 2, CityName = "City Two", ZIPCode = 67890 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _cityRepository.GetAllCities();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllCities_ShouldReturnEmptyList_WhenNoCitiesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _cityRepository.GetAllCities();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<City>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCityById_ShouldReturnCity_WhenCityExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int cityId = 1;
            var city = new City { CityID = cityId, CityName = "Existing City", ZIPCode = 12345 };
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cityRepository.GetCityById(cityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityId, result.CityID);
            Assert.Equal("Existing City", result.CityName);
        }

        [Fact]
        public async Task GetCityById_ShouldReturnNull_WhenCityDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _cityRepository.GetCityById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]          // This one fails. Might be from the repository or the test.
        public async Task UpdateCity_ShouldModifyCity_WhenCityExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int cityId = 1;
            var city = new City { CityID = cityId, CityName = "Old City Name", ZIPCode = 12345 };
            _context.City.Add(city);
            await _context.SaveChangesAsync();
            var updatedCity = new City { CityName = "Updated City Name", ZIPCode = 67890 };

            // Act
            var result = await _cityRepository.UpdateCity(updatedCity, cityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated City Name", result.CityName);
            Assert.Equal(67890, result.ZIPCode);
        }

        [Fact]
        public async Task UpdateCity_ShouldReturnNull_WhenCityDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var updatedCity = new City { CityID = 1, CityName = "Nonexistent City", ZIPCode = 99999 };

            // Act
            var result = await _cityRepository.UpdateCity(updatedCity, 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCity_ShouldRemoveCity_WhenCityExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int cityId = 1;
            var city = new City { CityID = cityId, CityName = "City to Delete", ZIPCode = 12345 };
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cityRepository.DeleteCity(cityId);
            var deletedCity = await _cityRepository.GetCityById(cityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityId, result.CityID);
            Assert.Null(deletedCity);
        }

        [Fact]
        public async Task DeleteCity_ShouldReturnNull_WhenCityDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _cityRepository.DeleteCity(1);

            // Assert
            Assert.Null(result);
        }
    }
}
