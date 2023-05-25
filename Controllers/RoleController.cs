using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound();
            }

            var data = roles.Select(_roleMapper.Map).ToList();

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound();
            }

            var data = _roleMapper.Map(role);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var roleConverted = _roleMapper.Map(roleVM);

            var result = _roleRepository.Create(roleConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
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
            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
