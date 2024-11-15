
using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Controllers
{
    public class CityControllerTest
    {
        private readonly Mock<ICityRepositories> _cityRepositoryMock;
        private readonly CityController _cityController;

        public CityControllerTest()
        {
            _cityRepositoryMock = new Mock<ICityRepositories>();
            _cityController = new CityController(_cityRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCities_ShouldReturnOkResultWithCities_WhenCitiesExist()
        {
            // Arrange
            var cities = new List<City>
            {
                new City { CityID = 1, CityName = "New York", ZIPCode = 10001 },
                new City { CityID = 2, CityName = "Los Angeles", ZIPCode = 90001 }
            };
            _cityRepositoryMock.Setup(repo => repo.GetAllCities()).ReturnsAsync(cities);

            // Act
            var result = await _cityController.GetCities();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCities = Assert.IsType<List<City>>(okResult.Value);
            Assert.Equal(2, returnedCities.Count);
        }

        [Fact]
        public async Task GetCities_ShouldReturnProblem_WhenCitiesIsNull()
        {
            // Arrange
            _cityRepositoryMock.Setup(repo => repo.GetAllCities()).ReturnsAsync((List<City>)null);

            // Act
            var result = await _cityController.GetCities();

            // Assert
            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, problemResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(problemResult.Value);
            Assert.Contains("unexpected", problemDetails.Detail);
        }


        [Fact]
        public async Task GetCitiesById_ShouldReturnOkResultWithCity_WhenCityExists()
        {
            // Arrange
            var city = new City { CityID = 1, CityName = "New York", ZIPCode = 10001 };
            _cityRepositoryMock.Setup(repo => repo.GetCityById(1)).ReturnsAsync(city);

            // Act
            var result = await _cityController.GetCitiesById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCity = Assert.IsType<City>(okResult.Value);
            Assert.Equal(city.CityID, returnedCity.CityID);
        }

        [Fact]
        public async Task GetCitiesById_ShouldReturnNotFound_WhenCityDoesNotExist()
        {
            // Arrange
            _cityRepositoryMock.Setup(repo => repo.GetCityById(1)).ReturnsAsync((City)null);

            // Act
            var result = await _cityController.GetCitiesById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("was not found", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task PostCity_ShouldReturnCreatedAtActionResult_WhenCityIsCreated()
        {
            // Arrange
            var newCity = new City { CityID = 1, CityName = "Chicago", ZIPCode = 60601 };
            _cityRepositoryMock.Setup(repo => repo.CreateCity(newCity)).ReturnsAsync(newCity);

            // Act
            var result = await _cityController.PostCity(newCity);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedCity = Assert.IsType<City>(createdAtActionResult.Value);
            Assert.Equal(newCity.CityID, returnedCity.CityID);
            Assert.Equal("Chicago", returnedCity.CityName);
        }

        [Fact]
        public async Task PostCity_ShouldReturnStatusCode500_WhenCityCreationFails()
        {
            // Arrange
            var newCity = new City { CityID = 1, CityName = "Chicago", ZIPCode = 60601 };
            _cityRepositoryMock.Setup(repo => repo.CreateCity(It.IsAny<City>())).ReturnsAsync((City)null);

            // Act
            var result = await _cityController.PostCity(newCity);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains("City was not created", statusCodeResult.Value.ToString());
        }

        [Fact]
        public async Task PutCity_ShouldReturnOkResult_WhenCityIsUpdated()
        {
            // Arrange
            var updatedCity = new City { CityID = 1, CityName = "Updated City", ZIPCode = 60601 };
            _cityRepositoryMock.Setup(repo => repo.UpdateCity(updatedCity, updatedCity.CityID)).ReturnsAsync(updatedCity);

            // Act
            var result = await _cityController.PutCity(updatedCity.CityID, updatedCity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCity = Assert.IsType<City>(okResult.Value);
            Assert.Equal(updatedCity.CityName, returnedCity.CityName);
        }

        [Fact]              // This one fails, there seems to be a problem in the controller
        public async Task PutCity_ShouldReturnNotFound_WhenCityDoesNotExist()
        {
            // Arrange
            var updatedCity = new City { CityID = 1, CityName = "Nonexistent City", ZIPCode = 99999 };
            _cityRepositoryMock.Setup(repo => repo.UpdateCity(updatedCity, updatedCity.CityID)).ReturnsAsync((City)null);

            // Act
            var result = await _cityController.PutCity(updatedCity.CityID, updatedCity);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("was not found", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteCity_ShouldReturnOkResultWithDeletedCity_WhenCityIsDeleted()
        {
            // Arrange
            var cityToDelete = new City { CityID = 1, CityName = "New York", ZIPCode = 10001 };
            _cityRepositoryMock.Setup(repo => repo.DeleteCity(cityToDelete.CityID)).ReturnsAsync(cityToDelete);

            // Act
            var result = await _cityController.DeleteCity(cityToDelete.CityID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCity = Assert.IsType<City>(okResult.Value);
            Assert.Equal(cityToDelete.CityID, returnedCity.CityID);
        }

        [Fact]
        public async Task DeleteCity_ShouldReturnNotFound_WhenCityDoesNotExist()
        {
            // Arrange
            _cityRepositoryMock.Setup(repo => repo.DeleteCity(1)).ReturnsAsync((City)null);

            // Act
            var result = await _cityController.DeleteCity(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("was not found", notFoundResult.Value.ToString());
        }
    }
}
