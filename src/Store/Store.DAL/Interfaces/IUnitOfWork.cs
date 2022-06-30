using Store.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IFoodRepository Foods { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        void Save();
        Task SaveAsync();
    }
}
