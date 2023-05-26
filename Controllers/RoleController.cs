using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.AccountRoles;
using API.ViewModels.Accounts;
using API.ViewModels.Employees;
using API.ViewModels.Response;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _roleMapper;
        public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> roleMapper)
        {
            _roleRepository = roleRepository;
            _roleMapper = roleMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Role Not Found",

                });
            }

            var data = roles.Select(_roleMapper.Map).ToList();
            return Ok(new ResponseVM<List<RoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Get All Data",
                Data = new List<RoleVM>(data)
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Role By Guid Not Found",

                });
            }
            var data = _roleMapper.Map(role);
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Get By Guid",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var roleConverted = _roleMapper.Map(roleVM);

            var result = _roleRepository.Create(roleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Create Role Failed"
                });
            }

            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Create Role Success"
            });
        }

        [HttpPut]
        public IActionResult Update(RoleVM roleVM)
        {
            var roleConverted = _roleMapper.Map(roleVM);

            var isUpdated = _roleRepository.Update(roleConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            var resultUpdateConverted = _roleMapper.Map(roleConverted);
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Update Account",
                Data = resultUpdateConverted
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Delete Role Failed"
                });
            }

            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Delete Role Success"
            });
        }
    }
}
