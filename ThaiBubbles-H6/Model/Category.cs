using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThaiBubbles_h6.Model
{

    public class Category
    {
        [Key]
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;



        public List<Product?> product { get; set; } = new List<Product?>();
    }
}



