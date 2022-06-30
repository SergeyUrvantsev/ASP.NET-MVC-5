using Store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.EF
{
    public class StoreDbInitializer
        : DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {


            context.Foods.Add(new Food { Id = 1, Name = "Margherita", Description = "Mozzarella cheese, red sauce, tomato", CategoryId = 1, Price = 220.00m });
            context.Foods.Add(new Food { Id = 2, Name = "Pepperoni", Description = "Pepperoni sausages, mozzarella cheese, red sauce, tomato", CategoryId = 1, Price = 220.00m });
            context.Foods.Add(new Food { Id = 3, Name = "Fried Unagi roll", Description = "Eel, cheese, rice, nori, crackers, batter", CategoryId = 2, Price = 199.00m });
            context.Foods.Add(new Food { Id = 4, Name = "Fried Kitami roll", Description = "Eel, ginger, cheese, unagi sauce", CategoryId = 2, Price = 199.00m });
            context.Foods.Add(new Food { Id = 5, Name = "Soy sauce", Description = "Soy sauce", CategoryId = 3, Price = 50.00m });

            context.Categories.Add(new Category { Id = 1, CategoryName = "Pizza" });
            context.Categories.Add(new Category { Id = 2, CategoryName = "Roll" });
            context.Categories.Add(new Category { Id = 3, CategoryName = "Sauce" });

            base.Seed(context);
        }
    }
}
