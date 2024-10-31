namespace ThaiBubbles_H6.Interfaces
{
    public interface IProductRepositories
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int productId);
        public Task<Product> CreateProduct(Product product);
        public Task<Product> UpdateProduct(Product product, int productId);
        public Task<Product> DeleteProduct(int productId);
    }
}
