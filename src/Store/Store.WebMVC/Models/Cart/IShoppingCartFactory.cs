using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.WebMVC.Models.Cart
{
    public interface IShoppingCartFactory
    {
        ShoppingCart GetCart(HttpContextBase context);
        ShoppingCart GetCart(Controller controller);
    }
}
