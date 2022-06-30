using AutoMapper;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class FoodService : IFoodService
    {
        private IUnitOfWork database;
        public FoodService(IUnitOfWork uow) =>
            database = uow;

        public IEnumerable<FoodDTO> FindFoods(string searchName)
        {
            var foods = database.Foods.Find(searchName);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Food, FoodDTO>()
                    .ForMember(dto => dto.Category, src => src
                    .MapFrom(b => b.CategorySection.CategoryName))).CreateMapper();
            return mapper.Map<IEnumerable<Food>, List<FoodDTO>>(foods);

        }

        public FoodDTO GetFood(int? id)
        {
            if (id == null)
                throw new ValidationException("id not set", "");
            var food = database.Foods.Get(id.Value);
            if (food == null)
                throw new ValidationException("product not found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Food, FoodDTO>()
                .ForMember(dto => dto.Category, src => src.MapFrom(b => b.CategorySection.CategoryName))
                .ForMember(dto => dto.CategoryId, src => src.MapFrom(b => b.CategorySection.Id))).CreateMapper();
            return mapper.Map<Food, FoodDTO>(database.Foods.Get(id.Value));

        }

        public IEnumerable<FoodDTO> GetFoods(string category)
        {
            IEnumerable<Food> foods;
            if (category != null)
            {
                foods = database.Foods.GetAll()
                    .Where(b => b.CategorySection.CategoryName == category);
            }
            else
            {
                foods = database.Foods.GetAll();
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Food, FoodDTO>()
                .ForMember(dto => dto.Category, src => src.MapFrom(b => b.CategorySection.CategoryName))).CreateMapper();
            return mapper.Map<IEnumerable<Food>, List<FoodDTO>>(foods);
        }

        public void DeleteFood(int id)
        {
            database.Foods.Delete(id);
            database.Save();
        }

        public void Update(FoodDTO foodDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FoodDTO, Food>()).CreateMapper();
            var food = mapper.Map<FoodDTO, Food>(foodDTO);
            database.Foods.Update(food);
            database.Save();
        }

        public void CreateFood(FoodDTO foodDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FoodDTO, Food>()).CreateMapper();
            var food = mapper.Map<FoodDTO, Food>(foodDTO);
            database.Foods.Create(food);
            database.Save();
        }

        public IEnumerable<FoodDTO> GetFoods()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Food, FoodDTO>()
                .ForMember(dto => dto.Category, src => src.MapFrom(b => b.CategorySection.CategoryName))).CreateMapper();
            return mapper.Map<IEnumerable<Food>, List<FoodDTO>>(database.Foods.GetAll());
        }
    }
}
