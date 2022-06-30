using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public string CartId { get; set; }
        public int FoodId { get; set; }
        public FoodDTO Food { get; set; }
        public int Count { get; set; }
    }
}
