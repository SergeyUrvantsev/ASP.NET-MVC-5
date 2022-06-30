using Microsoft.Owin.Security;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.WebMVC.Models;
using Store.WebMVC.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.WebMVC.Controllers
{
    public class AccountController : Controller
    {

        public IUserService UserService;
        public IOrderService OrderService;
        private readonly ShoppingCartFactory shoppingCartFactory;
        public const string CartSessionKey = "CartId";

        public AccountController(IUserService userService, IOrderService orderService, ShoppingCartFactory factory)
        {
            UserService = userService;
            OrderService = orderService;
            shoppingCartFactory = factory;
        }


        private IAuthenticationManager AuthenticationManager =>
            HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = viewModel.Email, Password = viewModel.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                    ModelState.AddModelError("", "Wrong login or password");
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    var cartId = shoppingCartFactory.GetCart(this.HttpContext).ShoppingCartId;
                    await MigrateShoppingCart(userDto.Email, cartId);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(viewModel);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();

        }

        public async Task MigrateShoppingCart(string userName, string CartId)
        {
            await OrderService.MigrateCart(userName, CartId);
            Session[CartSessionKey] = userName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Role = "user"

                };
                OperationDetails operationDetails = await UserService.Create(user);

                if (operationDetails.Succedeed)
                {
                    var cartId = shoppingCartFactory.GetCart(this.HttpContext).ShoppingCartId;
                    await MigrateShoppingCart(user.Email, cartId);
                    return View("SuccessRegister");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(viewModel);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                Password = "Admin123",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}