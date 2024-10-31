using System;
using System.ComponentModel.DataAnnotations;

namespace ThaiBubbles_H6.Model
{
    public class ProductList
    {
        [Key]
        public int ProductOrderListID { get; set; }

        public double Quantity { get; set; } = 0;

        public int? OrderId { get; set; }
        public Order? Orders { get; set; }

        public int? ProductId { get; set; }
        public Product? Products { get; set; }
    }
}