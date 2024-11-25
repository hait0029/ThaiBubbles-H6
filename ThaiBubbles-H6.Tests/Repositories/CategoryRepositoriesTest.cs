using Microsoft.EntityFrameworkCore;
using ThaiBubbles_H6.Repositories;
using ThaiBubbles_H6.Model;

namespace ThaiBubbles_H6.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private CategoryRepositories _categoryRepository;

        public CategoryRepositoryTests()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "CategoryRepository")
                .Options;

            _context = new DatabaseContext(_options);
            _categoryRepository = new CategoryRepositories(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfCategories_WhenCategoriesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Category.Add(new Category { CategoryID = 1, CategoryName = "Beverages" });
            _context.Category.Add(new Category { CategoryID = 2, CategoryName = "Snacks" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetAllCategory();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.GetAllCategory();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            _context.Category.Add(new Category { CategoryID = categoryId, CategoryName = "Beverages" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.CategoryID);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.GetCategoryById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddNewIdToCategory_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedNewId = 1;
            Category category = new() { CategoryID = expectedNewId, CategoryName = "Beverages" };

            // Act
            var result = await _categoryRepository.CreateCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(expectedNewId, result.CategoryID);
        }

        [Fact]
        public async Task Create_ShouldFailToAddCategory_WhenCategoryIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            Category category = new() { CategoryID = 1, CategoryName = "Beverages" };
            await _categoryRepository.CreateCategory(category);

            // Act
            async Task action() => await _categoryRepository.CreateCategory(category);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnCategory_WhenCategoryExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            _context.Category.Add(new Category { CategoryID = categoryId, CategoryName = "Beverages" });
            await _context.SaveChangesAsync();
            Category updatedCategory = new() { CategoryID = categoryId, CategoryName = "Updated Beverages" };

            // Act
            var result = await _categoryRepository.UpdateCategory(updatedCategory, categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(updatedCategory.CategoryName, result.CategoryName);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category updateCategory = new() { CategoryID = categoryId, CategoryName = "Updated Beverages" };

            // Act
            var result = await _categoryRepository.UpdateCategory(updateCategory, categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeletedCategory_WhenCategoryIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category category = new() { CategoryID = categoryId, CategoryName = "Beverages" };
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.DeleteCategory(categoryId);
            var deletedCategory = await _categoryRepository.GetCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.CategoryID);
            Assert.Null(deletedCategory);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.DeleteCategory(1);

            // Assert
            Assert.Null(result);
        }
    }
}
