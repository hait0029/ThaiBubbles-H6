namespace ThaiBubbles_H6.Tests.Controllers
{
    public class CategoryControllerTest
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<ICategoryRepositories> _categoryRepository = new();

        public CategoryControllerTest()
        {
            _categoryController = new CategoryController(_categoryRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCategoriesExists()
        {
            // Arrange
            List<Category> categories = new()
            {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "TestCategory1",
                    product = new List<Product>()
                }
            };

            _categoryRepository.Setup(x => x.GetAllCategory()).ReturnsAsync(categories);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.GetAllCategoryType();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenNoCategoriesExist()
        {
            // Arrange
            List<Category> categories = new();
            _categoryRepository.Setup(x => x.GetAllCategory()).ReturnsAsync(categories);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.GetAllCategoryType();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _categoryRepository.Setup(x => x.GetAllCategory()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.GetAllCategoryType();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category
            {
                CategoryID = categoryId,
                CategoryName = "TestCategory1",
                product = new List<Product>()
            };

            _categoryRepository.Setup(x => x.GetCategoryById(categoryId)).ReturnsAsync(category);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.GetCategoryById(categoryId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;
            _categoryRepository.Setup(x => x.GetCategoryById(categoryId)).ReturnsAsync((Category)null);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.GetCategoryById(categoryId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenCategoryIsCreated()
        {
            // Arrange
            var newCategory = new Category
            {
                CategoryID = 1,
                CategoryName = "TestCategory1",
                product = new List<Product>()
            };

            _categoryRepository.Setup(x => x.CreateCategory(It.IsAny<Category>())).ReturnsAsync(newCategory);

            // Act
            var result = await _categoryController.PostCategory(newCategory);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            var newCategory = new Category
            {
                CategoryID = 1,
                CategoryName = "TestCategory1",
                product = new List<Product>()
            };

            _categoryRepository.Setup(x => x.CreateCategory(It.IsAny<Category>())).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _categoryController.PostCategory(newCategory);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenCategoryIsUpdated()
        {
            // Arrange
            int categoryId = 1;
            var updatedCategory = new Category
            {
                CategoryID = categoryId,
                CategoryName = "UpdatedCategory",
                product = new List<Product>()
            };

            _categoryRepository.Setup(x => x.UpdateCategory(It.IsAny<Category>(), categoryId)).ReturnsAsync(updatedCategory);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.PutCategory(categoryId, updatedCategory);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int categoryId = 1;
            var updatedCategory = new Category
            {
                CategoryID = categoryId,
                CategoryName = "UpdatedCategory",
                product = new List<Product>()
            };

            _categoryRepository.Setup(x => x.UpdateCategory(It.IsAny<Category>(), categoryId)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.PutCategory(categoryId, updatedCategory);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenCategoryIsDeleted()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category
            {
                CategoryID = categoryId,
                CategoryName = "CategoryToDelete",
                product = new List<Product>()
            };

            _categoryRepository.Setup(x => x.DeleteCategory(categoryId)).ReturnsAsync(category);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.DeleteCategory(categoryId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;
            _categoryRepository.Setup(x => x.DeleteCategory(categoryId)).ReturnsAsync((Category)null);

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.DeleteCategory(categoryId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int categoryId = 1;
            _categoryRepository.Setup(x => x.DeleteCategory(categoryId)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = (IStatusCodeActionResult)await _categoryController.DeleteCategory(categoryId);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
