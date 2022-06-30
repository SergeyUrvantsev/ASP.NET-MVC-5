using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebMVC.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private IFoodService _foodService;
        private ICategoryService _categoryService;

        public CategoryController(IFoodService bookService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _foodService = bookService;
        }

        public ActionResult GetAllCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var categories =
                mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryService.GetCategories());
            return PartialView("ShowCategories", categories);
        }
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var categories =
                mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryService.GetCategories());
            return PartialView("ShowCategories", categories);
        }
        public ActionResult Edit(int id)
        {
            var categoryDTO = _categoryService.GetCategory(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var viewModel = mapper.Map<CategoryDTO, CategoryViewModel>(categoryDTO);
            return PartialView(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, CategoryDTO>())
                    .CreateMapper();
                var category = mapper.Map<CategoryViewModel, CategoryDTO>(viewModel);
                _categoryService.CreateCategory(category);
                return RedirectToAction("Main", "Food");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, CategoryDTO>()).CreateMapper();
                var category = mapper.Map<CategoryViewModel, CategoryDTO>(viewModel);
                _categoryService.Update(category);
                var mapperCategories = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
                var categories =
                    mapperCategories.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryService.GetCategories());
                return PartialView("ShowCategories", categories);

            }

            return PartialView("Edit", viewModel);
        }
    }
}