using FoodStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Food> Foods { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
