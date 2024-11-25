namespace ThaiBubbles_H6.Model
{

    public class Product
      {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; } = 0;

        public string Description { get; set; } = string.Empty;
        //public bool Favorite { get; set; } = false;

        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public Category? category { get; set; }



        [JsonIgnore]
        public List<ProductList?> orderlists { get; set; } = new List<ProductList?>();
      }
}

      

