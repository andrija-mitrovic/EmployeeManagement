using EmployeeManagement.Application.Common.DTOs;
using EmployeeManagement.Application.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Application.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Application.Employees.Queries.GetEmployeeById;
using EmployeeManagement.Application.Employees.Queries.GetEmployeesWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    public sealed class EmployeesController : ApiControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<EmployeeDto>>> GetProductsWithPagination([FromQuery] GetEmployeesWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var products = await Mediator.Send(query, cancellationToken);

            return products.Items!;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetProduct(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetEmployeeByIdQuery() { Id = id }, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            if (command.Id != id)
            {
                return BadRequest();
            }

            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteEmployeeCommand() { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
