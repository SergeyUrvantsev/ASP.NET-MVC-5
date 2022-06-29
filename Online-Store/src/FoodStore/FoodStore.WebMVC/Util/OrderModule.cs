using FoodStore.BLL.Interfaces;
using FoodStore.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.WebMVC.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}