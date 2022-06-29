using FoodStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.EF
{
    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<StoreDbContext>
    {
        protected override void Seed(StoreDbContext db)
        {
            db.Foods.Add(new Food { Name = "Margherita", Description = "Mozzarella cheese, red sauce, tomato", Price = 220.00m });
            db.Foods.Add(new Food { Name = "Pepperoni", Description = "Pepperoni sausages, mozzarella cheese, red sauce, tomato", Price = 399.00m });
            db.Foods.Add(new Food { Name = "Soy sauce", Description = "Soy sauce", Price = 25.00m });
            db.SaveChanges();
        }
    }
}
