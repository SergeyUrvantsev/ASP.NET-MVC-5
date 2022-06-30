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
    public class FoodRepository : IFoodRepository
    {
        private StoreContext db;
        public FoodRepository(StoreContext context) =>
            db = context;

        public void Create(Food item)
        {
            db.Foods.Add(item);
        }

        public void Delete(int Id)
        {
            Food food = db.Foods.Find(Id);
            if (food != null)
            {
                db.Foods.Remove(food);
            }
        }

        public IEnumerable<Food> Find(string searchString)
        {
            return db.Foods.Where(food => food.Name.Contains(searchString)).Include(food => food.CategorySection).ToList();
        }

        public Food Get(int id)
        {
            return db.Foods.Where(food => food.Id == id).Include(b => b.CategorySection).FirstOrDefault();
        }

        public IEnumerable<Food> GetAll()
        {
            return db.Foods.Include(b => b.CategorySection).ToList();
        }

        public void Update(Food item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
