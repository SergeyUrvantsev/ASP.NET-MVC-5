using AutoMapper;
using Store.BLL.DTO;
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
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork database;

        public CategoryService(IUnitOfWork uow) =>
            database = uow;


        public void CreateCategory(CategoryDTO categoryDto)
        {
            var category = new Category { CategoryName = categoryDto.CategoryName };
            database.Categories.Create(category);
            database.Save();
        }

        public void DeleteCategory(int id)
        {
            database.Categories.Delete(id);
            database.Save();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(database.Categories.GetAll());
        }

        public CategoryDTO GetCategory(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return mapper.Map<Category, CategoryDTO>(database.Categories.Get(id));
        }

        public void Update(CategoryDTO categoryDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
            var category = mapper.Map<CategoryDTO, Category>(categoryDto);
            database.Categories.Update(category);
            database.Save();

        }
    }
}
