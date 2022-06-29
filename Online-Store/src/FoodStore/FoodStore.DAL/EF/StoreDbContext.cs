using FoodStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.EF
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }

        static StoreDbContext()
        {
            Database.SetInitializer<StoreDbContext>(new StoreDbInitializer());
        }

        public StoreDbContext(string connectionString)
            : base(connectionString) { }
    }
}
