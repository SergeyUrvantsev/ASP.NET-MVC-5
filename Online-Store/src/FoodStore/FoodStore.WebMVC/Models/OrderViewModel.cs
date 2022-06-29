using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.WebMVC.Models
{
    public class OrderViewModel
    {
        public int FoodId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}