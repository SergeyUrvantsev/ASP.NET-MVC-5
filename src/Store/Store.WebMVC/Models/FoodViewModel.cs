using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.WebMVC.Models
{
    public class FoodViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the name of the dish")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a description of the dish")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter the price of the dish")]
        public decimal Price { get; set; }
        public string Category { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
            
    }
}