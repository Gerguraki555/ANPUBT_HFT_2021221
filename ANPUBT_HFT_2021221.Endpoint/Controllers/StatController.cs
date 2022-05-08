using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Logic;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IEmployeeLogic el;
        IGuestLogic gl;
        IRestaurantLogic rl;

        public StatController(IRestaurantLogic restaurantLogic,IEmployeeLogic employeeLogic,IGuestLogic guestLogic)
        {
            this.rl = restaurantLogic;
            this.gl = guestLogic;
            this.el = employeeLogic;
        }

        // /stat/hadmoretahoneguest
        [HttpGet]
        public IEnumerable<Employee> HadMoreThanOneGuest()
        {
            return el.HadMoreThanOneGuest();
        }

        // /stat/ThreeStarsOrHigherRatedRestaurantWorkers
        [HttpGet]
        public IEnumerable<Employee> ThreeStarsOrHigherRatedRestaurantWorkers()
        {
            return el.ThreeStarsOrHigherRatedRestaurantWorkers();
        }

        // /stat/italianoguestnames
        [HttpGet]
        public IEnumerable<string> ItalianoGuestNames()
        {
            return gl.ItalianoGuestNames();
        }
        
        // /stat/kirksguests
        [HttpGet]
        public IEnumerable<Guest> KirksGuests()
        {
            return gl.KirksGuests();
        }

        // /stat/...
        [HttpGet]
        public IEnumerable<int> RestaurantWorkerAVGSalaryMax()
        {
            return rl.RestaurantWorkerAVGSalaryMax();
        }

    }
}
