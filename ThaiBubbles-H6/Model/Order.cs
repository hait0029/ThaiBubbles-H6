using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThaiBubbles_h6.Model
{

    public class Order
    {
        [Key]
        public int OrderID { get; set; } = 0
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int? UserId { get; set; } 
        public User? user { get; set; }  

        [JsonIgnore]
        public List<ProductList?> orderlists { get; set; } = new List<ProductList?>();
    }
}



