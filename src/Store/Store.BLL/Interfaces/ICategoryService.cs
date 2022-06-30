using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        void Update(CategoryDTO categoryDto);
        void DeleteCategory(int id);
        void CreateCategory(CategoryDTO categoryDto);
    }
}
