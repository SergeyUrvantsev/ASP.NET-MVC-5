using Store.DAL.EF;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly StoreContext db;

        public CartRepository(StoreContext context) =>
            db = context;
        public async Task AddToCart(Food food, string Id)
        {
            var cartItem = await db.CartItems.SingleOrDefaultAsync(
                c => c.CartId == Id
                && c.FoodId == food.Id);

            if (cartItem == null)
            {

                cartItem = new CartItem
                {
                    FoodId = food.Id,
                    CartId = Id,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

        }
        

        public async Task Empty(string CartId)
        {
            var cartItems = await db.CartItems.Where(c => c.CartId == CartId).Include(x => x.Food).ToArrayAsync();
            db.CartItems.RemoveRange(cartItems);
        }

        public Task<List<CartItem>> GetAll(string Id)
        {
            return db.CartItems.Where(c => c.CartId == Id).Include(x => x.Food).ToListAsync();
        }

        public void Remove(int id, string CartId)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.CartId == CartId && cart.CartItemId == id);
            var itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
            }

        }
        public async Task CreateOrder(Order order, string CartId)
        {

            decimal orderTotal = 0;
            var cartItems = await GetAll(CartId);
            foreach (var item in cartItems)
            {
                var food = await db.Foods.SingleAsync(b => b.Id == item.FoodId);
                var orderDetail = new OrderDetail
                {
                    FoodId = item.FoodId,
                    OrderId = order.OrderId,
                    UnitPrice = food.Price,
                    Quantity = item.Count
                };
                orderTotal += (item.Count * food.Price);
                db.OrderDetails.Add(orderDetail);

            }
            order.Total = orderTotal;
            db.Orders.Add(order);
            await Empty(CartId);

        }

        public async Task MigrateCart(string userName, string CartId)
        {
            var cartItems = await db.CartItems.Where(c => c.CartId == CartId).ToListAsync();
            foreach (var item in cartItems)
            {
                item.CartId = userName;
            }
        }
    }
}
