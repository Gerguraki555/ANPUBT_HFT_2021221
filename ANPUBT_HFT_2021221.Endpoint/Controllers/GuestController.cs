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
    public class GuestController : ControllerBase
    {

        IGuestLogic gl;
        IHubContext<SignalRHub> hub;

        public GuestController(IGuestLogic guestLogic, IHubContext<SignalRHub> hub)
        {
            this.gl = guestLogic;
            this.hub = hub;
        }

        // GET: api/<GuestController>
        [HttpGet]
        public IEnumerable<Guest> Get()
        {
            return gl.ReadAll();
        }

        // GET api/<GuestController>/5
        [HttpGet("{id}")]
        public Guest Get(int id)
        {
            return gl.Read(id);
        }

        // POST api/<GuestController>
        [HttpPost]
        public void Post([FromBody] Guest value)
        {
            gl.Create(value);

            this.hub.Clients.All.SendAsync("GuestCreated", value);
        }

        // PUT api/<GuestController>/5
        //[HttpPut("{id}")]
        public void Put([FromBody] Guest value)
        {
            gl.Update(value);

            this.hub.Clients.All.SendAsync("GuestUpdated", value);
        }

        // DELETE api/<GuestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var guestToDelete = gl.Read(id);

            gl.Delete(id);

            this.hub.Clients.All.SendAsync("GuestDeleted", guestToDelete);
        }
    }
}
