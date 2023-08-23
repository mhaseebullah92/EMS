using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Commands.DeleteEmployeeById;
using Application.Features.Employees.Commands.UpdateEmployee;
using Application.Features.Employees.Queries.GetAllEmployees;
using Application.Features.Employees.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class EmployeeController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllEmployeesParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllEmployeesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteEmployeeByIdCommand { Id = id }));
        }
    }
}
