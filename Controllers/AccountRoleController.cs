using API.Contracts;
using API.Models;
using API.ViewModels.AccountRoles;
using API.ViewModels.Accounts;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountRoleController : BaseController<AccountRole, AccountRoleVM>
{
    public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
        : base(accountRoleRepository, mapper)
    {
    }
}