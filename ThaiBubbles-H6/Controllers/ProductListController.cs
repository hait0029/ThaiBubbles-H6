namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private IProductListRepositories _productListRepo;
        public ProductListController(IProductListRepositories temp)
        {
            _productListRepo = temp;
        }


        [HttpGet]
        public async Task<ActionResult> GetProductLists()
        {
            try
            {
                var productList = await _productListRepo.GetProductOrderList();
                if (productList == null)
                {
                    return Problem("Nothing was returned from productlist , this is unexpected");
                }
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("{productlistId}")]
        public async Task<ActionResult> GetProductstById(int productlistId)
        {
            try
            {
                var productlist = await _productListRepo.GetProductOrderListById(productlistId);

                if (productlist == null)
                {
                    return NotFound($"ProductList with id {productlistId} was not found");
                }
                return Ok(productlist);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{productlistId}")]
        public async Task<ActionResult> PutProductList(int productlistId, ProductList productlist)
        {
            try
            {
                var ProductListResult = await _productListRepo.UpdateProductOrderList(productlistId, productlist);

                if (ProductListResult == null)
                {
                    return NotFound($"ProductList with id {productlistId} was not found");
                }
                return Ok(ProductListResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostProductList(ProductList productlist)
        {
            try
            {
               var createProductList = await _productListRepo.CreateProductOrderList(productlist);
                if (createProductList == null)
                {
                    return StatusCode(500, "productList was not created. Something failed...");
                }
                return CreatedAtAction("PostProductList", new {productlistid = createProductList.ProductOrderListID }, createProductList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the productList {ex.Message}");
            }
        }

        [HttpDelete("{productListId}")]
        public async Task<ActionResult> DeleteProductList(int productlistId)
        {
            try
            {
                var productlist = await _productListRepo.DeleteProductOrderList(productlistId);

                if (productlist == null)
                {
                    return NotFound($"ProductList with id {productlistId} was not found");
                }
                return Ok(productlist);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
