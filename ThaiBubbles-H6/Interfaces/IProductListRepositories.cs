namespace ThaiBubbles_H6.Interfaces
{
    public interface IProductListRepositories
    {
        public Task<List<ProductList>> GetProductOrderList();
        public Task<ProductList> GetProductOrderListById(int productOrderListId);
        public Task<ProductList> CreateProductOrderList(ProductList productOrderList);
        public Task<ProductList> UpdateProductOrderList( int productOrderListId, ProductList productOrderList);
        public Task<ProductList> DeleteProductOrderList(int productOrderListId);

    }
}
