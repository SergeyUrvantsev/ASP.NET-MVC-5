using AutoMapper;
using FoodStore.BLL.DTO;
using FoodStore.BLL.Infrastructure;
using FoodStore.BLL.Interfaces;
using FoodStore.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        IOrderService orderService;
        public HomeController(IOrderService service) =>
            orderService = service;


        public ActionResult Index()
        {
            IEnumerable<FoodDTO> foodDtos = orderService.GetFoods();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FoodDTO, FoodViewModel>()).CreateMapper();
            var foods = mapper.Map<IEnumerable<FoodDTO>, List<FoodViewModel>>(foodDtos);
            return View(foods);
        }

        public ActionResult MakeOrder(int? id)
        {
            try
            {
                FoodDTO food = orderService.GetFood(id);
                var order = new OrderViewModel { FoodId = food.Id };

                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                var orderDto = new OrderDTO { FoodId = order.FoodId, Address = order.Address, PhoneNumber = order.PhoneNumber };
                orderService.MakeOrder(orderDto);
                return Content("<h2>Your order has been successfully placed</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Index();
        }
        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }
    }
}