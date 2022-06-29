using FoodStore.DAL.Entities;
using FoodStore.DAL.Interfaces;
using FoodStore.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FoodStore.DAL.Repositories
{
    public class FoodRepository : IRepository<Food>
    {
        private StoreDbContext DbContext;

        public FoodRepository(StoreDbContext context) =>
            DbContext = context;

        public IEnumerable<Food> GetAll()
        {
            return DbContext.Foods;
        }

        public Food Get(int id)
        {
            return DbContext.Foods.Find(id);
        }


        public void Create(Food item)
        {
            DbContext.Foods.Add(item);
        }

        public void Delete(int id)
        {
            Food food = DbContext.Foods.Find(id);
            if (food != null)
                DbContext.Foods.Remove(food);
        }



        public void Update(Food food)
        {
            DbContext.Entry(food).State = EntityState.Modified;
        }

        public IEnumerable<Food> Find(Func<Food, bool> predicate)
        {
            return DbContext.Foods.Where(predicate).ToList();
        }
    }
}
