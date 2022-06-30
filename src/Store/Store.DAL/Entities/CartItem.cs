using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
    public class CartItem
    {

        public int CartItemId { get; set; }


        public string CartId { get; set; }
        public int FoodId { get; set; }
        public int Count { get; set; }


        public DateTime DateCreated { get; set; }
        public virtual Food Food { get; set; }
    }
}
