using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ANPUBT_HFT_2021221.Logic;
using ANPUBT_HFT_2021221.Repository;
using ANPUBT_HFT_2021221.Models;
using Moq;

namespace ANPUBT_HFT_2021221.Test
{
    [TestFixture]
    public class Test
    {
        EmployeeLogic eLogic;
        RestaurantLogic rLogic;
        GuestLogic gLogic;
        public Test()
        {

            var employees = new List<Employee>().AsQueryable();
            var restaurants = new List<Restaurant>().AsQueryable();
            var guests = new List<Guest>().AsQueryable();

            List<Employee> emps = new List<Employee>();
            List<Guest> gue = new List<Guest>();
            List<Restaurant> res = new List<Restaurant>();

            #region filling lists with datas



            Food GulyasSoup = new Food()
            {
                Name = "Gulyás leves",
                Price = 2500
            };
            Food MeatSoup = new Food()
            {
                Name = "Húsleves",
                Price = 2000
            };
            Food Pizza = new Food()
            {
                Name = "Pizza",
                Price = 1800
            };
            Food Pasta = new Food()
            {
                Name = "Pasta",
                Price = 1200
            };

            Restaurant Soupaurant = new Restaurant()
            {
                Restaurant_id = 1,
                RestaurantName = "Soupaurant",
                Foodlist = new List<Food>(),
                Rating = 4
            };

            Soupaurant.Foodlist.ToList().Add(GulyasSoup);
            Soupaurant.Foodlist.ToList().Add(MeatSoup);

            Restaurant Italiano = new Restaurant()
            {
                Restaurant_id = 2,
                RestaurantName = "Italiano",
                Foodlist = new List<Food>(),
                Rating = 5,
            };

            Italiano.Foodlist.ToList().Add(Pizza);
            Italiano.Foodlist.ToList().Add(Pasta);

            Employee ItalianoBob = new Employee()
            {
                EmployeeId = 1,
                Restaurant = Italiano,
                Name = "Bob",
                Salary = 250000,
                RestaurantId = Italiano.Restaurant_id
            };

            Employee ItalianoMario = new Employee()
            {
                EmployeeId = 2,
                RestaurantId = Italiano.Restaurant_id,
                Restaurant = Italiano,
                Name = "Mario",
                Salary = 255000,
            };

            Employee SoupDan = new Employee()
            {
                EmployeeId = 3,
                Name = "Dan",
                Salary = 300000,
                RestaurantId = Soupaurant.Restaurant_id,
                Restaurant = Soupaurant
            };

            Employee SoupKirk = new Employee()
            {
                EmployeeId = 4,
                Name = "Kirk",
                Salary = 350000,
                RestaurantId = Soupaurant.Restaurant_id,
                Restaurant = Soupaurant
            };

            Guest JH = new Guest()
            {
                Name = "James Hetfield",
                Number = "06201111213",
                Email = "metallica.james.hetfield@gmail.com",
                Employee = SoupKirk,
                DeliveredFood = MeatSoup,
                GuestId = 4,
                OrderId = SoupKirk.EmployeeId
            };

            Guest LU = new Guest()
            {
                Name = "Lars Ulrich",
                Number = "06203451134",
                Email = "metallica.lars.ulrich@gmail.com",
                Employee = SoupKirk,
                DeliveredFood = GulyasSoup,
                GuestId = 3,
                OrderId = SoupKirk.EmployeeId
            };

            Guest SG = new Guest()
            {
                Name = "Synyster Gates",
                Number = "06 20 123 4432",
                Email = "avengedsevenfold.synister.gates@gmail.com",
                Employee = ItalianoMario,
                DeliveredFood = Pizza,
                GuestId = 2,
                OrderId = ItalianoMario.EmployeeId
            };

            Guest MS = new Guest()
            {
                Name = "Matt Shadows",
                Number = "06 20 678 9945",
                Email = "avengedsevenfold.synister.gates@gmail.com",
                Employee = ItalianoBob,
                DeliveredFood = Pasta,
                GuestId = 1,
                OrderId = ItalianoBob.EmployeeId
            };

            SoupKirk.Guests.Add(JH);
            SoupKirk.Guests.Add(LU);

            ItalianoBob.Guests.Add(SG);
            ItalianoMario.Guests.Add(MS);

            Soupaurant.Employees.Add(SoupKirk);
            Soupaurant.Employees.Add(SoupDan);

            Italiano.Employees.Add(ItalianoMario);
            Italiano.Employees.Add(ItalianoBob);

            emps.Add(SoupKirk);
            emps.Add(SoupDan);
            emps.Add(ItalianoBob);
            emps.Add(ItalianoMario);

            gue.Add(JH);
            gue.Add(LU);
            gue.Add(SG);
            gue.Add(MS);

            res.Add(Italiano);
            res.Add(Soupaurant);

            #endregion

            employees = emps.AsQueryable();
            restaurants = res.AsQueryable();
            guests = gue.AsQueryable();

            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var mockRestaurantRepo = new Mock<IRestaurantRepository>();
            var mockGuestRepo = new Mock<IGuestRepository>();

            mockEmployeeRepo.Setup(e => e.ReadAll()).Returns(employees);
            mockRestaurantRepo.Setup(r => r.ReadAll()).Returns(restaurants);
            mockGuestRepo.Setup(g => g.ReadAll()).Returns(guests);
            ;
            eLogic = new EmployeeLogic(mockEmployeeRepo.Object);
            rLogic = new RestaurantLogic(mockRestaurantRepo.Object);
            gLogic = new GuestLogic(mockGuestRepo.Object);
        }
        [Test]
        public void EmployeeUpdateMethodTest()
        {
            //Testing what happens if you don't give a value to e.employeeId(There's no employee who has 0 as his employeeId)
            Employee e = new Employee();
            e.Salary = 1000;

            Assert.That(() => eLogic.Update(e), Throws.Exception);
        }

        [Test]
        public void GuestUpdateMethodTest()
        {
            Guest g = new Guest();
            g.Number = "062044121221";

            Assert.That(() => gLogic.Update(g), Throws.Exception);
        }

        [Test]
        public void RestaurantUpdateMethodTest()
        {
            Restaurant r = new Restaurant();
            r.Rating =1;

            Assert.That(() => rLogic.Update(r), Throws.Exception);
        }

        [Test]
        public void HadMoreThanOneGuestTest()
        {
            var employee = eLogic.HadMoreThanOneGuest().ToArray();

            Assert.That(employee[0].Name, Is.EqualTo("Kirk"));

        }

        [Test]
        public void RestaurantWorkerAVGSalaryMaxTest()
        {
            var prices = rLogic.RestaurantWorkerAVGSalaryMax().ToArray();

            var soupprice= rLogic.ReadAll().Where(x => x.RestaurantName.Equals("Soupaurant")).Select(m => m.Employees.Max(s => s.Salary)).ToArray();
            var ital = rLogic.ReadAll().Where(x => x.RestaurantName.Equals("Italiano")).Select(m=>m.Employees.Max(s=>s.Salary)).ToArray();


            Assert.That(prices[1], Is.EqualTo(soupprice[0]));
            Assert.That(prices[0], Is.EqualTo(ital[0]));

        }

        [Test]
        public void ItalianoGuestsName()
        {
            var names = gLogic.ItalianoGuestNames().ToArray();

            Assert.That(names[0], Is.EqualTo("Synyster Gates"));
            Assert.That(names[1], Is.EqualTo("Matt Shadows"));


        }

        [Test]
        public void KirksGuestsTest()
        {
            var guests = gLogic.KirksGuests().ToArray();

            Assert.That(guests[0].Name, Is.EqualTo("James Hetfield"));
            Assert.That(guests[1].Name, Is.EqualTo("Lars Ulrich"));

        }

        [Test]
        public void ThreeStarsOrHigherRatedRestaurantWorkersTest()
        {
            var employees = eLogic.ThreeStarsOrHigherRatedRestaurantWorkers().ToArray();

            Assert.That(employees[0].Name, Is.EqualTo("Kirk"));
            Assert.That(employees[1].Name, Is.EqualTo("Dan"));
            Assert.That(employees[2].Name, Is.EqualTo("Bob"));
            Assert.That(employees[3].Name, Is.EqualTo("Mario"));
        }



        [Test]
        public void EmployeeCreateMethodTest()
        {
            Employee e = new Employee();
            e.Salary = 1000;

            Assert.That(() => eLogic.Create(e), Throws.Exception);
        }
        [Test]
        public void GuestCreateMethodTest()
        {
            Guest g = new Guest();
            g.Email = "Bob@hotmail.com";

            Assert.That(() => gLogic.Create(g), Throws.Exception);
        }
        [Test]
        public void RestaurantCreateMethodTest()
        {
            Restaurant r = new Restaurant();
            r.Rating = 3;

            Assert.That(() => rLogic.Create(r), Throws.Exception);
        }

        [Test]
        public void DatabaseConnectionsWorking()
        {
            var FromRestaurantToEmployee = rLogic.ReadAll().Select(x => x.Employees).ToArray();
            var FromEmployeeToGuests = eLogic.ReadAll().Select(x => x.Guests).ToArray();
            var FromGuestsToEmployee = gLogic.ReadAll().Select(x => x.Employee.Name).ToArray();

            Assert.That(FromRestaurantToEmployee[0].Count, Is.EqualTo(2));
            Assert.That(FromEmployeeToGuests[1].Count, Is.EqualTo(0));
            Assert.That(FromGuestsToEmployee[0], Is.EqualTo("Kirk"));


        }

    }
}
