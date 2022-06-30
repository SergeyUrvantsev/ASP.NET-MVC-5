using AutoMapper;
using Store.BLL.DTO;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }
        public OrderService(IUnitOfWork uow) =>
            Database = uow;


        
        public async Task AddToCart(FoodDTO cartItem, string Id)
        {
            Food food = new Food()
            {
                Id = cartItem.Id,
                Name = cartItem.Name,
                Description = cartItem.Description,
                Price = cartItem.Price
            };
            
            await Database.Carts.AddToCart(food, Id);
            await Database.SaveAsync();

        }

        public async Task<List<CartItemDTO>> GetAllCartItems(string Id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CartItem, CartItemDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<CartItem>, List<CartItemDTO>>(await Database.Carts.GetAll(Id));
        }

        public void RemoveFromCart(int id, string CartId)
        {
            Database.Carts.Remove(id, CartId);
            Database.Save();
        }

        public async Task<decimal> GetTotal(string CartId)
        {
            var cartItems = await Database.Carts.GetAll(CartId);
            decimal total = cartItems.Select(c => c.Food.Price * c.Count).Sum();
            return total;
        }
        public async Task EmptyCart(string CartId)
        {
            var cartItems = await Database.Carts.GetAll(CartId);
            await Database.Carts.Empty(CartId);
            await Database.SaveAsync();
        }
        public async Task MigrateCart(string userName, string CartId)
        {
            await Database.Carts.MigrateCart(userName, CartId);
            await Database.SaveAsync();
        }


        public async Task CreateNewOrder(OrderDTO orderDTO, string CartId)
        {
            
            var order = new Order
            {
                OrderDate = orderDTO.OrderDate,
                UserName = orderDTO.UserName,
                FirstName = orderDTO.FirstName,
                LastName = orderDTO.LastName,
                Address = orderDTO.Address,
                City = orderDTO.City,
                Phone = orderDTO.Phone,
                Email = orderDTO.Email
            };
            

            await Database.Carts.CreateOrder(order, CartId);
            await Database.SaveAsync();
            
        }

        private bool IsAnyNullOrEmpty(OrderDTO order)
        {
            foreach (PropertyInfo pi in order.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(order);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
