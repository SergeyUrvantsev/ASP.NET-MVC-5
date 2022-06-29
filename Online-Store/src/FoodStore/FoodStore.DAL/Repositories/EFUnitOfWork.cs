using FoodStore.DAL.EF;
using FoodStore.DAL.Entities;
using FoodStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private StoreDbContext db;
        private FoodRepository foodRepository;
        private OrderRepository orderRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new StoreDbContext(connectionString);
        }

        public IRepository<Food> Foods
        {
            get
            {
                if (foodRepository == null)
                    foodRepository = new FoodRepository(db);
                return foodRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
