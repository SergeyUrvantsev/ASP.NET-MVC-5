using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebMVC.Models
{
    public class CreateFoodViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the name of the dish")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a description of the dish")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter the price of the dish")]
        [Range(0.00, 5000.00, ErrorMessage = "Incorrect price value entered")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Select a category")]
        public int CategoryId { get; set; }

        public SelectList Categories { get; set; }

        public string img { get; set; }
    }
}