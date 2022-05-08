using System.Linq;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Repository
{
    public interface IRestaurantRepository
    {
        void Create(Restaurant rest);
        void Delete(int id);
        Restaurant Read(int id);
        IQueryable<Restaurant> ReadAll();
        void Update(Restaurant rest);
    }
}