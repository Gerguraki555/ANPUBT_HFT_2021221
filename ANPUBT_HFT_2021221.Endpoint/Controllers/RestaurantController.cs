using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Endpoint.Services;
using ANPUBT_HFT_2021221.Logic;
using ANPUBT_HFT_2021221.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ANPUBT_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        IRestaurantLogic rl;
        IHubContext<SignalRHub> hub;

        public RestaurantController(IRestaurantLogic restaurantLogic, IHubContext<SignalRHub> hub)
        {
            this.rl = restaurantLogic;
            this.hub = hub;
        }

        // GET: api/<RestaurantController>
        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return rl.ReadAll();
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{id}")]
        public Restaurant Get(int id)
        {
            return rl.Read(id);
        }

        // POST api/<RestaurantController>
        [HttpPost]
        public void Post([FromBody] Restaurant value)
        {
            rl.Create(value);
            this.hub.Clients.All.SendAsync("RestaurantCreated", value);
        }

        // PUT api/<RestaurantController>/5
        //[HttpPut("{id}")]
        public void Put([FromBody] Restaurant value)
        {
            rl.Update(value);

            this.hub.Clients.All.SendAsync("RestaurantUpdated", value);
        }

        // DELETE api/<RestaurantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var restaurantToDelete=rl.Read(id);

            rl.Delete(id);

            this.hub.Clients.All.SendAsync("RestaurantDeleted", restaurantToDelete);
        }
    }
}
