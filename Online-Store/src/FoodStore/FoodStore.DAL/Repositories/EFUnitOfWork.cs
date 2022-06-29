using FoodStore.DAL.EF;
using FoodStore.DAL.Entities;
using FoodStore.DAL.Identity;
using FoodStore.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private StoreDbContext _db;

        private FoodRepository _foodRepository;
        private OrderRepository _orderRepository;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientManager _clientManager;

        public EFUnitOfWork(string connectionString)
        {
            _db = new StoreDbContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
            _clientManager = new ClientManager(_db);
        }

        public ApplicationUserManager UserManager => _userManager;

        public IClientManager ClientManager => _clientManager;

        public ApplicationRoleManager RoleManager => _roleManager;


        public IRepository<Food> Foods
        {
            get
            {
                if (_foodRepository == null)
                    _foodRepository = new FoodRepository(_db);
                return _foodRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_db);
                return _orderRepository;
            }
        }


        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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
