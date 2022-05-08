using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Models;
using ZC7ADM_HFT_2021221.Data;

namespace ANPUBT_HFT_2021221.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        RestaurantDbContext db;

        public RestaurantRepository(RestaurantDbContext db)
        {
            this.db = db;
        }


        public void Create(Restaurant rest)
        {
            db.Restaurants.Add(rest);
            db.SaveChanges();
        }

        public Restaurant Read(int id)
        {
            return db.Restaurants.FirstOrDefault(f => f.Restaurant_id == id);
        }

        public IQueryable<Restaurant> ReadAll()
        {
            return db.Restaurants;
        }
        public void Delete(int id)
        {
            db.Restaurants.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Restaurant rest)
        {
            var oldrest = Read(rest.Restaurant_id);
            oldrest.Employees = rest.Employees;
            oldrest.Foodlist = rest.Foodlist;
            oldrest.Rating = rest.Rating;
            oldrest.RestaurantName = rest.RestaurantName;
            oldrest.Restaurant_id = rest.Restaurant_id;
            db.SaveChanges();

        }

    }
}
