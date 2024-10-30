namespace ThaiBubbles_H6.Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        private DatabaseContext _context { get; set; }
        private IProductListRepositories _productListRepositories { get; set; }

        public OrderRepositories(DatabaseContext context, IProductListRepositories productListRepositories)
        {
            _context = context;
            _productListRepositories = productListRepositories;
        }

        public async Task<Order> CreateOrder(Order newOrder) 
        {
            if (newOrder.UserId.HasValue) 
            { 
                newOrder.user = await _context.User.FirstOrDefaultAsync(e => e.UserID == newOrder.UserId);
            }

            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();

            if(newOrder.orderlists != null && newOrder.orderlists.Count > 0)
            {
                foreach (var productListItem in newOrder.orderlists)
                {
                    productListItem.OrderId = newOrder.OrderID;
                    await _productListRepositories.CreateProductOrderList(productListItem);
                }
            }
            return newOrder;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId) 
        {
            return await _context.Order.FirstOrDefaultAsync(e => e.OrderID == orderId);
        }

        public async Task<Order> UpdateOrder(int orderId, Order updateOrder)
        {
            Order order = await GetOrderById(orderId);

            if (order != null && updateOrder != null)
            {
                order.OrderID = updateOrder.OrderID;
                order.OrderDate = updateOrder.OrderDate;

                if(updateOrder.user != null)
                {
                    order.user = await _context.User.FirstOrDefaultAsync(e => e.UserID == updateOrder.UserId);
                }
                else
                {
                    order.user = null;
                }

                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return await GetOrderById(orderId);
        }


        public async Task<Order> DeleteOrder(int orderId)
        {
            Order order = await GetOrderById(orderId);
            if (order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }   

    }
}
