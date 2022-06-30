using AutoMapper;
using PagedList;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.WebMVC.Models;
using Store.WebMVC.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public IOrderService orderService;
        public IFoodService foodService;
        private readonly ShoppingCartFactory shoppingCartFactory;

        public HomeController(IOrderService oService, IFoodService bService, ShoppingCartFactory factory)
        {
            orderService = oService;
            shoppingCartFactory = factory;
            foodService = bService;
        }
        public ActionResult Index(int? id, string category, int? page, string searchName)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            IEnumerable<FoodDTO> foods;

            ViewBag.category = category;
            ViewBag.searchName = searchName;

            if (searchName == null)
                foods = foodService.GetFoods(category);
            else
                foods = foodService.FindFoods(searchName);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FoodDTO, FoodViewModel>()).CreateMapper();
            var foodsViewModel = mapper.Map<IEnumerable<FoodDTO>, List<FoodViewModel>>(foods);


            if (Request.IsAjaxRequest())
            {
                var cart = shoppingCartFactory.GetCart(HttpContext);
                var addedFood = foodService.GetFood(id.Value);
                orderService.AddToCart(addedFood, cart.ShoppingCartId);


                return PartialView("FoodSummary", foodsViewModel.ToPagedList(pageNumber, pageSize));
            }

            return View(foodsViewModel.ToPagedList(pageNumber, pageSize));
        }
        public async Task<ActionResult> AddToCart(int? id, int? page, string category, string author)
        {
            var cart = shoppingCartFactory.GetCart(HttpContext);
            var addedFood = foodService.GetFood(id.Value);
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            await orderService.AddToCart(addedFood, cart.ShoppingCartId);

            var foods = foodService.GetFoods(category);

            return PartialView("FoodSummary", foods.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Autocomplete(string term)
        {
            var foods = foodService.FindFoods(term).Select(x => new { value = x.Name }).Distinct();
            return Json(foods, JsonRequestBehavior.AllowGet);
        }
    }
}