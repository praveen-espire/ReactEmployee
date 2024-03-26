using Entities.BizEntities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    public IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService EmployeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = EmployeeService;
        _logger = logger;

    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogDebug("GetAllCutomers Called");

        var employees = await _employeeService.GetAllEmployees();
        return Ok(new { results = employees });
        //return _employeeService.GetAllEmployees().Result;
    }

    [HttpGet("{EmployeeId}")]
    public async Task<IActionResult> GetById(int EmployeeId)
    {
        _logger.LogDebug("GetEmployeeByID Called");

        var response = await _employeeService.GetEmployeeByID(EmployeeId);
        return Ok(new { results = response });
    }

    [HttpDelete("{EmployeeId}")]
    public async Task<IActionResult> Delete(int EmployeeId)
    {
        _logger.LogDebug("DeleteEmployeeByID Called");

        var response = await _employeeService.DeleteEmployeeByID(EmployeeId);
        return Ok(new { results = response });
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] EmployeeBizEntity EmployeeBizEntity)
    {
        _logger.LogDebug("UpdateEmployee Called");

        var response = await _employeeService.UpdateEmployee(EmployeeBizEntity);
        if (response == null)
            return BadRequest(new { message = "Unable to update Employee." });

        return Ok(new { results = response });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EmployeeBizEntity EmployeeBizEntities)
    {
        _logger.LogDebug("InsertEmployee Called");

        var response = await _employeeService.InsertEmployee(EmployeeBizEntities);
        if (response == null)
            return BadRequest(new { message = "Unable to insert Employee." });

        return Ok(new { results = response });
    }
}
