using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Models;
using ZC7ADM_HFT_2021221.Data;

namespace ANPUBT_HFT_2021221.Repository
{
    public class GuestRepository : IGuestRepository
    {

        RestaurantDbContext db;

        public GuestRepository(RestaurantDbContext db)
        {
            this.db = db;
        }


        public void Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
        }

        public Guest Read(int id)
        {
            return db.Guests.FirstOrDefault(f => f.GuestId == id);
        }

        public IQueryable<Guest> ReadAll()
        {
            return db.Guests;
        }
        public void Delete(int id)
        {
            db.Guests.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Guest guest)
        {
            var oldrest = Read(guest.GuestId);
            oldrest.Name = guest.Name;
            oldrest.DeliveredFood = guest.DeliveredFood;
            oldrest.Email = guest.Email;
            oldrest.Employee = guest.Employee;
            oldrest.GuestId = guest.GuestId;
            oldrest.Number = guest.Number;
            oldrest.OrderId = guest.OrderId;
            db.SaveChanges();

        }

    }
}
