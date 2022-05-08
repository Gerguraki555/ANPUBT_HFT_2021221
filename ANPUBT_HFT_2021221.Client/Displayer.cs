using ANPUBT_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANPUBT_HFT_2021221.Client
{
    static class Displayer
    {
        public static void GuestDisplayer(RestService rest)
        {
            var guests = rest.Get<Guest>("guest");
            foreach (var item in guests)
            {
                Console.WriteLine("---Guests---");
                Console.WriteLine("Guest's Name: " + item.Name);
                Console.WriteLine("Guest's Number: " + item.Number);
                Console.WriteLine("Guest's Email: " + item.Email);
                Console.WriteLine("Guest's GuestId: " + item.GuestId);
            }
            Console.WriteLine("Press any button to return to main menu!");
            Console.ReadLine();
        }
        public static void EmployeesDisplayer(RestService rest)
        {
            var employees = rest.Get<Employee>("employee");

            foreach (var item in employees)
            {
                Console.WriteLine("----Employees----");
                Console.WriteLine("Employees Name: " + (item as Employee).Name);
                Console.WriteLine("Employees Salary: " + (item as Employee).Salary);
                Console.WriteLine("Employee's Id: " + item.EmployeeId);
                item.Guests.ToList().ForEach(g => Console.WriteLine("Guest's served: " + g.Name));
            }
            Console.WriteLine("Press any button to return to main menu!");
            Console.ReadLine();
        }
        public static void HadMoreThanOneGuestDisplayer(RestService rest)
        {
            var employees = rest.Get<Employee>("/stat/hadmorethanoneguest");
            foreach (var item in employees)
            {
                Console.WriteLine("----Employees----");
                Console.WriteLine("Employees Name: " + (item as Employee).Name);
                Console.WriteLine("Employees Salary: " + (item as Employee).Salary);
                item.Guests.ToList().ForEach(g => Console.WriteLine("Guest's served: " + g.Name));
            }
            Console.WriteLine("Press any button to return to main menu!");
            Console.ReadLine();

        }
        public static void KirsksGuestsDisplayer(RestService rest)
        {
            var guests = rest.Get<Guest>("/stat/kirksguests");
            foreach (var item in guests)
            {
                Console.WriteLine("---Guests---");
                Console.WriteLine("Guest's Name: " + item.Name);
                Console.WriteLine("Guest's Number: " + item.Number);
                Console.WriteLine("Guest's Email: " + item.Email);
            }
            Console.WriteLine("Press any button to return to main menu!");
            Console.ReadLine();

        }
        public static void RestaurantDisplayer(RestService rest)
        {
            var restaurant = rest.Get<Restaurant>("restaurant");

            foreach (var item in restaurant)
            {
                Console.WriteLine("---Restaurant---");
                Console.WriteLine("Restaurants Name: " + item.RestaurantName);
                Console.WriteLine("Restaurants Rating: " + item.RestaurantName);
                Console.WriteLine("Restaurant's Id: " + item.Restaurant_id);
                item.Employees.ToList().ForEach(e => Console.WriteLine("Employee's Name: " + e.Name));

            }
            Console.WriteLine("Press any button to return to main menu!");
            Console.ReadLine();

        }
        public static void ThreeStarsOrHigherRatedRestaurantWorkersDisplayer(RestService rest)
        {
            var employees = rest.Get<Employee>("/stat/ThreeStarsOrHigherRatedRestaurantWorkers");
            foreach (var item in employees)
            {
                Console.WriteLine("----Employees----");
                Console.WriteLine("Employees Name: " + (item as Employee).Name);
                Console.WriteLine("Employees Salary: " + (item as Employee).Salary);
                item.Guests.ToList().ForEach(g => Console.WriteLine("Guest's served: " + g.Name));
            }
            Console.WriteLine("Press any button to return to main menu!");
            Console.ReadLine();

        }
        public static void RestaurantWorkerAVGSalaryMax(RestService rest)
        {
            var avg = rest.Get<int>("/stat/RestaurantWorkerAVGSalaryMax");
            Console.WriteLine("Max Salaries are: ");
            foreach (var item in avg)
            {
                Console.WriteLine("Salary: " + item);
            }
            Console.WriteLine("Press any key to return to menu!");
            Console.ReadLine();
        }
        public static void ItalianoGuestNamesDisplayer(RestService rest)
        {
            var names = rest.Get<string>("/stat/ItalianoGuestNames");
            foreach (var item in names)
            {
                Console.WriteLine("Name: " + item);
            }
            Console.WriteLine("Press any key to return to menu!");
            Console.ReadLine();
        }
        private static Employee MakeEmployee()
        {
            Employee e = new Employee();
            Console.WriteLine("Insert Employee's Name: ");
            string name = Console.ReadLine();
            e.Name = name;
            Console.WriteLine("Insert Employee's Salary: ");
            int salary = int.Parse(Console.ReadLine());
            e.Salary = salary;
            Console.WriteLine("Restaurant's id: ");
            e.RestaurantId = int.Parse(Console.ReadLine());
            Console.WriteLine("Would you like to insert Guests? y/n");
            char ans = char.Parse(Console.ReadLine());
            if (ans == 'y')
            {
                Console.WriteLine("How many?");
                int number = int.Parse(Console.ReadLine());
                for (int i = 0; i < number; i++)
                {
                    e.Guests.Add(MakeGuest());
                }
            }
            return e;
        }
        public static void CreateEmployeeMethod(RestService rest)
        {
            Employee e = MakeEmployee();
            rest.Post<Employee>(e, "employee");
        }
        public static void CreateGuestMethod(RestService rest)
        {
            Guest g = MakeGuest();
            ;
            rest.Post<Guest>(g, "guest");

        }
        private static Guest MakeGuest()
        {
            Guest g = new Guest();

            Console.WriteLine("Insert Name: ");
            string name = Console.ReadLine();
            g.Name = name;

            Console.WriteLine("Insert Email: ");
            string email = Console.ReadLine();
            g.Email = email;

            Console.WriteLine("Insert number: ");
            string number = Console.ReadLine();
            g.Number = number;

            Console.WriteLine("Insert OrderId: ");
            g.OrderId = int.Parse(Console.ReadLine());
            Console.WriteLine("Insert Food name:");

            Food f = new Food();
            f.Name = Console.ReadLine();
            Console.WriteLine("Insert Food's price: ");
            f.Price = int.Parse(Console.ReadLine());

            g.DeliveredFood = f;


            return g;
        }
        public static void DeleteGuestMethod(RestService rest)
        {
            Console.WriteLine("Insert an Id to delete a Guest:");
            int number = int.Parse(Console.ReadLine());
            rest.Delete(number, "guest");
        }
        public static void DeleteRestaurantMethod(RestService rest)
        {
            Console.WriteLine("Insert an Id to delete a Restaurant:");
            int number = int.Parse(Console.ReadLine());
            rest.Delete(number, "restaurant");
        }
        public static void DeleteEmployeeMethod(RestService rest)
        {
            Console.WriteLine("Insert an Id to delete an Employee:");
            int number = int.Parse(Console.ReadLine());
            rest.Delete(number, "employee");
        }
        public static void CreateRestaurantMethod(RestService rest)
        {
            Restaurant r = MakeRestaurant();
            rest.Post<Restaurant>(r, "restaurant");
        }
        private static Restaurant MakeRestaurant() 
        {
            Restaurant r = new Restaurant();

            Console.WriteLine("Insert Name: ");
            r.RestaurantName = Console.ReadLine();

            Console.WriteLine("Insert rating: ");
            r.Rating = int.Parse(Console.ReadLine());

            Console.WriteLine("Would you like to add foods? y/n");
            char ans = char.Parse(Console.ReadLine());
            if (ans == 'y')
            {
                Console.WriteLine("How many?");
                int number = int.Parse(Console.ReadLine());
                for (int i = 0; i < number; i++)
                {
                    Food f = new Food();
                    Console.WriteLine("Insert Food's Name");
                    f.Name = Console.ReadLine();
                    Console.WriteLine("Insert Food's price");
                    f.Price = int.Parse(Console.ReadLine());
                    r.Foodlist.ToList().Add(f);
                }
            }

            Console.WriteLine("Would you like to add Employees? y/n");
            ans = ' ';
            ans = char.Parse(Console.ReadLine());
            if (ans == 'y')
            {
                Console.WriteLine("How many?");
                int number = int.Parse(Console.ReadLine());
                for (int i = 0; i < number; i++)
                {
                    r.Employees.Add(MakeEmployee());
                }
            }
            return r;
        }
        public static void UpdateRestaurantMethod(RestService rest) 
        {
            Console.WriteLine("---Making a new Restaurant---");
            Restaurant r = MakeRestaurant();
            Console.WriteLine("Restaurant's Id: ");
            r.Restaurant_id =int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------");
            Console.WriteLine("Wich restaurant would you like to update?");
            int number = int.Parse(Console.ReadLine());
            rest.Put<Restaurant>(r,"restaurant/"+number);        
        }
        public static void UpdateEmployeeMethod(RestService rest) 
        {
            Console.WriteLine("---Making a new Restaurant---");
            Employee e = MakeEmployee();
            Console.WriteLine("Employee's id: ");
            e.EmployeeId = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------");
            rest.Put<Employee>(e, "employee/"+e.EmployeeId);
        }
        public static void UpdateGuestMethod(RestService rest)
        {
            Console.WriteLine("---Making a new Guest---");
            Guest g = MakeGuest();
            Console.WriteLine("Guest's Id:");
            g.GuestId = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------");
            Console.WriteLine("Wich guest would you like to update?");
            rest.Put<Guest>(g, "guest/"+g.GuestId);
        }

    }
}
