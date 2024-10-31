namespace ThaiBubbles_H6.Interfaces
{
    public interface IOrderRepositories
    {
        public Task<List<Order>> GetAllOrders();
        public Task<Order> GetOrderById(int orderId);
        public Task<Order> CreateOrder(Order order);
        public Task<Order> UpdateOrder(int orderId, Order order);
        public Task<Order> DeleteOrder(int orderId);
    }
}
