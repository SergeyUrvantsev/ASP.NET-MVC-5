using FoodStore.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.EF
{
    public class StoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        static StoreDbContext()
        {
            Database.SetInitializer<StoreDbContext>(new StoreDbInitializer());
        }

        public StoreDbContext(string connectionString)
            : base(connectionString) { }
    }
}
