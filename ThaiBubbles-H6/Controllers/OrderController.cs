namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepositories _orderRepo;
        public OrderController(IOrderRepositories temp)
        {
            _orderRepo = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderRepo.GetAllOrders();

                if (orders == null)
                {
                    return Problem("Nothing was returned from orders, this is unexpected");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrdersById(int orderId)
        {
            try
            {
                var order = await _orderRepo.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound($"Order with id {orderId} was not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> UpdateOrder(int orderId,Order order)
        {
            try
            {
                var OrderResult = await _orderRepo.UpdateOrder(orderId, order);

                if (order == null)
                {
                    return NotFound($"Order with id {orderId} was not found");
                }
                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder(Order order)
        {
            try
            {
                var CreateOrder = await _orderRepo.CreateOrder(order);

                if (CreateOrder == null)
                {
                    return StatusCode(500, "Order was not created. Something failed...");
                }
                return CreatedAtAction("postOrder", new {orderId = CreateOrder.OrderID}, CreateOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the order {ex.Message}");
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _orderRepo.DeleteOrder(orderId);

                if (order == null)
                {
                    return NotFound($"Order with id {orderId} was not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
