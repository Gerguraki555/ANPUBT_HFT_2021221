using System.Collections.Generic;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Logic
{
    public interface IRestaurantLogic
    {
        IEnumerable<int> RestaurantWorkerAVGSalaryMax();
        void Create(Restaurant rest);
        void Delete(int id);
        Restaurant Read(int id);
        IEnumerable<Restaurant> ReadAll();
        void Update(Restaurant rest);
    }
}