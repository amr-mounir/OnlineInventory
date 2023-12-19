using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineInventory.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name must not exceed 100 characters.")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0.")]
        public decimal ProductPrice { get; set; }

        [StringLength(1000, ErrorMessage = "Product description must not exceed 1000 characters.")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Product quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product quantity must be at least 1.")]
        public int ProductQuantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

