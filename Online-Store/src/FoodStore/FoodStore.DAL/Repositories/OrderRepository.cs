using FoodStore.DAL.EF;
using FoodStore.DAL.Entities;
using FoodStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private StoreDbContext DbContext;

        public OrderRepository(StoreDbContext context) =>
            DbContext = context;


        public void Create(Order order)
        {
            DbContext.Orders.Add(order);
        }

        public void Delete(int id)
        {
            Order order = DbContext.Orders.Find(id);
            if (order != null)
                DbContext.Orders.Remove(order);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return DbContext.Orders.Include(o => o.Food).Where(predicate).ToList();
        }

        public Order Get(int id)
        {
            return DbContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return DbContext.Orders.Include(o => o.Food);
        }

        public void Update(Order order)
        {
            DbContext.Entry(order).State = EntityState.Modified;
        }
    }
}
