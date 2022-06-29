using FoodStore.DAL.Entities;
using FoodStore.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }

        IRepository<Food> Foods { get; }
        IRepository<Order> Orders { get; }
        Task Save();
    }
}
