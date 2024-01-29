using bmsgateway.cqrs.commands;
using bmsgateway.cqrs.queries;
using bmsgateway.models;
using bmsgateway.repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bmsgateway.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly IMediator _mediator;
    public EmployeesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetEmployeeListQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery() { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
    {
        var result = await _mediator.Send(
            new CreateEmployeeCommand(employee.Id, employee.Name,
        employee.Email, employee.Address ?? "", employee.DateOfBirth)
            {
                Email = employee.Email,
                Name = employee.Name
            });

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
    {
        var result = await _mediator.Send(
            new UpdateEmployeeCommand(employee.Id, employee.Name,
        employee.Email, employee.Address ?? "", employee.DateOfBirth)
            {
                Email = employee.Email,
                Name = employee.Name
            });

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _mediator.Send(new DeleteEmployeeCommand() { Id = id });

        return Ok(result);
    }
}