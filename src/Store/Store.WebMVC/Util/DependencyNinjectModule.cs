using Ninject.Modules;
using Store.BLL.Interfaces;
using Store.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Store.WebMVC.Util
{
    public class DependencyNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UserService>();
            Bind<IFoodService>().To<FoodService>();
            Kernel.Unbind<ModelValidatorProvider>();
        }
    }
}