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
    public class EmployeeController : ControllerBase
    {
        IEmployeeLogic eLogic;

        IHubContext<SignalRHub> hub;

        public EmployeeController(IEmployeeLogic employeeLogic, IHubContext<SignalRHub> hub)
        {
            this.eLogic = employeeLogic;
            this.hub = hub;
        }

        // GET: /employee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return eLogic.ReadAll();
        }

        // GET employees/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return eLogic.Read(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
            this.eLogic.Create(value);

            this.hub.Clients.All.SendAsync("EmployeeCreated",value);
        }

        // PUT api/<ValuesController>/5
        //[HttpPut]
        public void Put([FromBody] Employee value)
        {
            this.eLogic.Update(value);

            this.hub.Clients.All.SendAsync("EmployeeUpdated", value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var employeetoDelete = this.eLogic.Read(id);

            this.eLogic.Delete(id);

            this.hub.Clients.All.SendAsync("EmployeeDeleted", employeetoDelete);
        }
    }
}
