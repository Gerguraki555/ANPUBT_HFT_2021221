using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Models;
using ZC7ADM_HFT_2021221.Data;

namespace ANPUBT_HFT_2021221.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        RestaurantDbContext db;

        public EmployeeRepository(RestaurantDbContext db)
        {
            this.db = db;
        }


        public void Create(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
        }

        public Employee Read(int id)
        {
            return db.Employees.FirstOrDefault(f => f.EmployeeId == id);
        }

        public IQueryable<Employee> ReadAll()
        {
            return db.Employees;
        }
        public void Delete(int id)
        {
            db.Employees.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Employee e)
        {
            var oldrest = Read(e.EmployeeId);
            oldrest.Name = e.Name;
            oldrest.Guests = e.Guests;
            oldrest.Restaurant = e.Restaurant;
            oldrest.RestaurantId = e.RestaurantId;
            oldrest.Salary = e.Salary;
            oldrest.EmployeeId = e.EmployeeId;
            db.SaveChanges();

        }


    }
}
