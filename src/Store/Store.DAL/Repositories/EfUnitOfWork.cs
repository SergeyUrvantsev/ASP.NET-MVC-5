using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.EF;
using Store.DAL.Entities;
using Store.DAL.Identity;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private StoreContext db;
        private IFoodRepository _foodRepository;
        private ICategoryRepository _categoryRepository;
        private ICartRepository _cartRepository;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public EfUnitOfWork(string connectionString)
        {
            db = new StoreContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
        }
        public ApplicationUserManager UserManager => _userManager;

        public ApplicationRoleManager RoleManager => _roleManager;
        public IFoodRepository Books
        {
            get
            {
                if (_foodRepository == null)
                    _foodRepository = new _foodRepository(db);
                return _foodRepository;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(db);
                return _categoryRepository;
            }
        }

        public ICartRepository Carts
        {
            get
            {
                if (_cartRepository == null)
                    _cartRepository = new CartRepository(db);
                return _cartRepository;
            }
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

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Save()
        {

            db.SaveChanges();
        }


    }
}
