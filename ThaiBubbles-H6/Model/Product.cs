using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ThaiBubbles_H6.Model;

namespace ThaiBubbles_h6.Model
{

      public class Product
      {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;

        public int price { get; set; } = 0;

        public string Description { get; set; } = string.Empty;
        public bool Favorite { get; set; } = false;

        public int? CategoryId { get; set; }
        public Category? category { get; set; }



        [JsonIgnore]
        public List<ProductList?> orderlists { get; set; } = new List<ProductList?>();
      }
}

      

