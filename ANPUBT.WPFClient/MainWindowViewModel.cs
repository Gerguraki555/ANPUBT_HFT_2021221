using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        //http://localhost:31877/

        private Employee selectedEmployee;

        private Restaurant selectedRestaurant;

        private Guest selectedguest;

        public Guest Selectedguest
        {
            get { return selectedguest; }
            set
            {
                if (value != null)
                {
                    selectedguest = new Guest()
                    {
                        Name=value.Name,
                        Email= value.Email,
                        DeliveredFood= value.DeliveredFood,
                        Employee= value.Employee,
                        GuestId= value.GuestId,
                        OrderId= value.OrderId,
                        Number= value.Number
                    };
                    OnPropertyChanged();
                    (DeleteGuestCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateGuestCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }

        }



        public Restaurant SelectedRestaurant
        {
            get { return selectedRestaurant; }
            set
            {
                if (value != null)
                {
                    selectedRestaurant = new Restaurant()
                    {
                        Restaurant_id=value.Restaurant_id,
                        RestaurantName=value.RestaurantName,
                        Rating=value.Restaurant_id,
                        Employees=value.Employees,
                        Foodlist=value.Foodlist
                    };
                    OnPropertyChanged();
                    (DeleteRestaurantCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateRestaurantCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }

        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (value != null)
                {
                    selectedEmployee = new Employee()
                    {
                        Name = value.Name,
                        EmployeeId = value.EmployeeId,
                        Salary = value.Salary,
                        Guests = value.Guests,
                        Restaurant = value.Restaurant,
                        RestaurantId = value.RestaurantId
                    };
                    OnPropertyChanged();
                    (DeleteEmployeeCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateEmployeeCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
           
        }


        public RestCollection<Employee> Employees { get; set; }
        public RestCollection<Restaurant> Restaurants { get; set; }
        public RestCollection<Guest> Guests { get; set; }

        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand CreateRestaurantCommand { get; set; }
        public ICommand DeleteRestaurantCommand { get; set; }
        public ICommand UpdateRestaurantCommand { get; set; }
        public ICommand CreateGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand UpdateGuestCommand { get; set; }

        public MainWindowViewModel()
        {
            this.Employees = new RestCollection<Employee>("http://localhost:31877/","employee","hub");
            this.Restaurants = new RestCollection<Restaurant>("http://localhost:31877/", "restaurant", "hub");
            this.Guests = new RestCollection<Guest>("http://localhost:31877/", "guest", "hub");


            CreateGuestCommand = new RelayCommand(() =>
            {
                Guests.Add(new Guest()
                {
                    Name=selectedguest.Name,
                    Number=selectedguest.Number,
                    Email=selectedguest.Name,
                    OrderId=selectedguest.OrderId

                });
            });

            CreateRestaurantCommand = new RelayCommand(() =>
               {
                   Restaurants.Add(new Restaurant()
                   {
                       RestaurantName = selectedRestaurant.RestaurantName,
                       Rating = selectedRestaurant.Rating

                   }) ;
               });

            CreateEmployeeCommand = new RelayCommand(() =>
              {
                  Employees.Add(new Employee()
                  {
                      Name = selectedEmployee.Name,
                      RestaurantId = selectedEmployee.RestaurantId,
                      Salary = selectedEmployee.Salary
                  });
              });

            DeleteGuestCommand = new RelayCommand(() =>
            {

                Guests.Delete(selectedguest.GuestId);
            },
           () => { return selectedguest != null; }
           );

            DeleteRestaurantCommand = new RelayCommand(() =>
            {

                Restaurants.Delete(selectedRestaurant.Restaurant_id);
            },
            () => { return selectedRestaurant != null; }
            );

            DeleteEmployeeCommand = new RelayCommand(() =>
            {

                Employees.Delete(SelectedEmployee.EmployeeId);
            },
            () => { return SelectedEmployee != null; }
            );

            UpdateGuestCommand = new RelayCommand(() =>
            {
                Guests.Update(selectedguest);
            });

            UpdateEmployeeCommand = new RelayCommand(() =>
              {
                  Employees.Update(SelectedEmployee);
              });

            UpdateRestaurantCommand = new RelayCommand(() =>
            {
                Restaurants.Update(selectedRestaurant);
            });

            this.SelectedEmployee = new Employee() { Name = "Teszt" };
            this.SelectedRestaurant = new Restaurant() { RestaurantName = "Teszt" };
            this.selectedguest = new Guest() { Name = "Teszt" };
        }
    }
}
