using API.Contracts;
using API.Models;
using API.ViewModels.AccountRoles;
using API.ViewModels.Accounts;
using API.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _accountRoleMapper;
        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> accountRoleMapper)
        {
            _accountRoleRepository = accountRoleRepository;
            _accountRoleMapper = accountRoleMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return NotFound(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Account Not Found",
                });
            }

            var data = accountRoles.Select(_accountRoleMapper.Map).ToList();

            return Ok(new ResponseVM<List<AccountRoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Get All Data",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return NotFound(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Messages = "Account Not Found",

                });
            }

            var data = _accountRoleMapper.Map(accountRole);
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Get By Guid",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountRoleVM)
        {
            var accountRoleConverted = _accountRoleMapper.Map(accountRoleVM);

            var result = _accountRoleRepository.Create(accountRoleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Created Account Role Failed",
                    Data = null
                });
            }

            var resultConverted = _accountRoleMapper.Map(result);
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success Create Account Role",
                Data = resultConverted
            });

        }

        [HttpPut]
        public IActionResult Update(AccountRoleVM accountRoleVM)
        {
            var accountRoleConverted = _accountRoleMapper.Map(accountRoleVM);

            var isUpdated = _accountRoleRepository.Update(accountRoleConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Failed to Update Account Role",
                });
            }
            var resultUpdateConverted = _accountRoleMapper.Map(accountRoleConverted);
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Success",
                Data = resultUpdateConverted
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRoleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Messages = "Failed to Delete Account Role",
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Messages = "Delete Account Success"
            });
        }

    }
}
