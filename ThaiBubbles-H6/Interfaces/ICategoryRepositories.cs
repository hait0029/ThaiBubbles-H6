namespace ThaiBubbles_H6.Interfaces
{
    public interface ICategoryRepositories
    {
        public Task<List<Category>> GetAllCategory();
        public Task<Category> GetCategoryById(int categoryId);
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(Category category, int categoryId);
        public Task<Category> DeleteCategory(int categoryId);
    }
}
