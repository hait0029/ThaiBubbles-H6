namespace ThaiBubbles_H6.Repositories
{
    public class ProductListRepositories : IProductListRepositories
    {
        private DatabaseContext _context { get; set; }

        public ProductListRepositories(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<ProductList> CreateProductOrderList(ProductList newProductOrderList)
        {

            if (newProductOrderList.OrderId.HasValue)
            {
                var order = await _context.Order
                    .FirstOrDefaultAsync(e => e.OrderID == newProductOrderList.OrderId);

                if (order != null)
                {
                    newProductOrderList.Orders = order;
                }
                else
                {
                    throw new ArgumentException($"Order with ID {newProductOrderList.OrderId} does not exist.");
                }
            }


            if (newProductOrderList.ProductId.HasValue)
            {
                var product = await _context.Product
                    .FirstOrDefaultAsync(e => e.ProductID == newProductOrderList.ProductId);

                if (product != null)
                {
                    newProductOrderList.Products = product;
                }
                else
                {
                    throw new ArgumentException($"Product with ID {newProductOrderList.ProductId} does not exist.");
                }
            }

            _context.ProductList.Add(newProductOrderList);

            await _context.SaveChangesAsync();

            return newProductOrderList;
        }





        public async Task<List<ProductList>> GetProductOrderList()
        {
            return await _context.ProductList.Include(e => e.Orders).Include(e => e.Products).ToListAsync();
        }




        public async Task<ProductList> GetProductOrderListById(int productOrderListId)
        {
            return await _context.ProductList.FirstOrDefaultAsync(e => e.ProductOrderListID == productOrderListId);
        }




        public async Task<ProductList> UpdateProductOrderList(int productOrderListId, ProductList updateProductOrderList)
        {
            ProductList productList = await GetProductOrderListById(productOrderListId);
            if (productList != null)
            {
                productList.ProductOrderListID = updateProductOrderList.ProductOrderListID;
                productList.Quantity = updateProductOrderList.Quantity;


                if(updateProductOrderList.Products != null)
                {
                    productList.Products = await _context.Product.FirstOrDefaultAsync(e => e.ProductID == updateProductOrderList.Products.ProductID);
                }
                else
                {
                    productList.Products = null;
                }

                if(updateProductOrderList.Orders != null)
                {
                    productList.Orders = await _context.Order.FirstOrDefaultAsync(e => e.OrderID == updateProductOrderList.Orders.OrderID);
                }
                else
                {
                    productList.Orders = null;
                }

                await _context.SaveChangesAsync();
            }
            return productList;
        }

        public async Task<ProductList> DeleteProductOrderList(int productOrderListId)
        {
            ProductList productList = await GetProductOrderListById(productOrderListId);
            if (productList != null)
            {
                _context.ProductList.Remove(productList);
                await _context.SaveChangesAsync();
            }
            return productList;
        }


    }
}
