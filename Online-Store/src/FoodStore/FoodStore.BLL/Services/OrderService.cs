using AutoMapper;
using FoodStore.BLL.BusinessModels;
using FoodStore.BLL.DTO;
using FoodStore.BLL.Infrastructure;
using FoodStore.BLL.Interfaces;
using FoodStore.DAL.Entities;
using FoodStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow) =>
            Database = uow;

        public void MakeOrder(OrderDTO orderDto)
        {
            Food food = Database.Foods.Get(orderDto.FoodId);


            if (food == null)
                throw new ValidationException("Product not found", "");



            decimal sum = new Discount(0.1m).GetDiscountedPrice(food.Price);

            Order order = new Order
            {
                Date = DateTime.Now,
                Address = orderDto.Address,
                FoodId = food.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };
            Database.Orders.Create(order);
            Database.Save();
        }

        public IEnumerable<FoodDTO> GetFoods()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Food, FoodDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Food>, List<FoodDTO>>(Database.Foods.GetAll());
        }

        public FoodDTO GetFood(int? id)
        {
            if (id == null)
                throw new ValidationException("Id not set", "");
            var food = Database.Foods.Get(id.Value);
            if (food == null)
                throw new ValidationException("Product not found", "");

            return new FoodDTO { Description = food.Description, Id = food.Id,
                Name = food.Name, Price = food.Price };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
