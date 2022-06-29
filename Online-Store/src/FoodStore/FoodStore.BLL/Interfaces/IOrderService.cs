using FoodStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.BLL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        FoodDTO GetFood(int? id);
        IEnumerable<FoodDTO> GetFoods();
        void Dispose();
    }
}
