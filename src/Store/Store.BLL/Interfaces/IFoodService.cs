using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface IFoodService
    {
        IEnumerable<FoodDTO> FindFoods(string searchName);
        FoodDTO GetFood(int? id);
        IEnumerable<FoodDTO> GetFoods(string category);
        IEnumerable<FoodDTO> GetFoods();
        void DeleteFood(int id);
        void Update(FoodDTO foodDTO);
        void CreateFood(FoodDTO foodDTO);
    }
}
