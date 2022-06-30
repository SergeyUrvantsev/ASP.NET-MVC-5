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
    public class FoodController : Controller
    {
        private IFoodService _foodService;
        private ICategoryService _categoryService;

        public FoodController(IFoodService bookService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _foodService = bookService;
        }

        public ActionResult Main()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FoodDTO, FoodViewModel>()).CreateMapper();
            var foods = _foodService.GetFoods();

            return View(foods);
        }



        public ActionResult GetAllFoods()
        {
            var foods = _foodService.GetFoods();
            return PartialView("ShowFoods", foods);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var food = _foodService.GetFood(id);
            _foodService.DeleteFood(id);
            var foods = _foodService.GetFoods();
            return PartialView("ShowFoods", foods);
        }

        public ActionResult Edit(int id)
        {
            var food = _foodService.GetFood(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FoodDTO, CreateFoodViewModel>()).CreateMapper();
            var viewModel = mapper.Map<FoodDTO, CreateFoodViewModel>(food);
            viewModel.Categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName");
            return PartialView(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateFoodViewModel viewModel, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    viewModel.ImageMimeType = image.ContentType;
                    viewModel.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(viewModel.ImageData, 0, image.ContentLength);
                }

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreateFoodViewModel, FoodDTO>()).CreateMapper();
                var foodDTO = mapper.Map<CreateFoodViewModel, FoodDTO>(viewModel);
                _foodService.Update(foodDTO);
                var foods = _foodService.GetFoods();

                return PartialView("ShowFoods", foods);

            }
            else
            {
                ModelState.AddModelError("", "Data filled out incorrectly");
            }
            viewModel.Categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName");
            return PartialView("Edit", viewModel);

        }

        public FileContentResult GetImage(int id)
        {
            var food = _foodService.GetFood(id);

            if (food != null & food.ImageData != null
                & food.ImageMimeType != null & food.ImageMimeType != "")
            {
                return File(food.ImageData, food.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Create()
        {

            CreateFoodViewModel viewModel = new CreateFoodViewModel
            {
                Categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFoodViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                FoodDTO foodDTO = new FoodDTO
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    CategoryId = viewModel.CategoryId,
                    Description = viewModel.Description,
                };

                _foodService.CreateFood(foodDTO);
                return RedirectToAction("Main");
            }
            else
            {
                ModelState.AddModelError("", "The entered data is incorrect");
            }

            return View(viewModel);
        }
    }
}