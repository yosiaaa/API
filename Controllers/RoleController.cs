using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Employees;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : BaseController<Role, RoleVM>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RoleVM> _mapper;
    public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper) : base(roleRepository, mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

}