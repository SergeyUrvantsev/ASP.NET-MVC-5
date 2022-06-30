using Store.BLL.Interfaces;
using Store.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebMVC.Controllers
{
    public class NavController : Controller
    {
        private IOrderService _orderService;
        private ICategoryService _categoryService;
        public NavController(IOrderService orderService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _orderService = orderService;
        }

        public ActionResult GetNav(string category = null)
        {
            var categories = _categoryService.GetCategories();
            MenuViewModel viewModel = new MenuViewModel
            {
                Categories = categories.ToList()
            };
            ViewBag.SelectedCategegory = category;
            return PartialView(viewModel);
        }
    }
}