namespace ThaiBubbles_H6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private ICategoryRepositories _categoryRepo;
        public CategoryController(ICategoryRepositories temp)
        {
            _categoryRepo = temp;
        }



        [HttpGet]


        public async Task<ActionResult> GetAllCategoryType()
        {
            try
            {
                var category = await _categoryRepo.GetCategory();
                if (category == null) 
                { 
                    return Problem("Nothing was returned from category service, this is unexpected");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }



        [HttpGet("{categoryId}")]


        public async Task<ActionResult> GetCategoryById(int categoryId)
        {
            try
            {
                var category = await _categoryRepo.GetCategoryById(categoryId);

                if (category == null)
                {
                    return NotFound($"Category with id {categoryId} was not found");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpPut("{categoryId}")]



        public async Task<ActionResult> PutCategory(int categoryId, Category category)
        {
            try
            {
                var categoryResult = await _categoryRepo.UpdateCategory(category, categoryId);

                if (category == null)
                {
                    return NotFound($"Category with id {categoryId} was not found");
                }
                return Ok(categoryResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpPost]



        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            try
            {
                var createCategory = await _categoryRepo.CreateCategory(category);

                if (category == null)
                {
                    return StatusCode(500, "Category was not created. Something failed...");
                }
                return CreatedAtAction("PostCategory", new {categoryId = createCategory.CategoryID},createCategory);
            }
            catch (Exception ex)
            {
               return StatusCode(500,$" An erroe occured while creating the category type {ex.Message}");
            }
        }



        [HttpDelete("{categoryId}")]


        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                var category = await _categoryRepo.DeleteCategory(categoryId);

                if (category == null)
                {
                    return NotFound($"Category with id {categoryId} was not found");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
