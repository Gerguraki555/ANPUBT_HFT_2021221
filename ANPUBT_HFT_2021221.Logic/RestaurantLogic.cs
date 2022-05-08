using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Repository;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Logic
{
    public class RestaurantLogic : IRestaurantLogic
    {
        IRestaurantRepository restRepo;

        public RestaurantLogic(IRestaurantRepository restrepo)
        {
            this.restRepo = restrepo;
        }

        public void Create(Restaurant rest)
        {
            if (rest.RestaurantName == null)
            {
                throw new ArgumentNullException("A restaurant must have a name!");
            }
            restRepo.Create(rest);
        }

        public Restaurant Read(int id)
        {
            return restRepo.Read(id);
        }

        public IEnumerable<Restaurant> ReadAll()
        {
            return restRepo.ReadAll();
        }
        public void Delete(int id)
        {
            restRepo.Delete(id);
        }

        public void Update(Restaurant rest)
        {
            if (rest.Restaurant_id==0||rest.RestaurantName==null)
            {
                throw new ArgumentNullException("RestaurantId or Restaurant name must be set!");
            }
            restRepo.Update(rest);
        }

        //non-crud methods

        public IEnumerable<int> RestaurantWorkerAVGSalaryMax()
        {
            return restRepo.ReadAll().Select(x=>x.Employees.Max(m=>m.Salary));                   
        }

    }
}
