using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Accounts;
using API.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using API.ViewModels.Others;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : BaseController<Employee, EmployeeVM>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IMapper<Employee, EmployeeVM> _mapper;
    public EmployeeController(IEmployeeRepository employeeRepository, IEducationRepository educationRepository,
            IUniversityRepository universityRepository, IMapper<Employee, EmployeeVM> mapper) : base(employeeRepository, mapper)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
        _mapper = mapper;
    }


    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAllEmployee()
    {
        var masterEmployees = _employeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Get All Master Employee not Found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = masterEmployees
        });
    }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid(Guid guid)
    {
        var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
        if (masterEmployees is null)
        {
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Get Master Employee by Guid not Found",
                Data = null
            });
        }

        return Ok(new ResponseVM<MasterEmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = masterEmployees
        });
    }

}