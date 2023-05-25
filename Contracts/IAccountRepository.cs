using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        int Register(RegisterVM registerVM);
        LoginVM Login(LoginVM loginVM);
        int UpdateOTP(Guid? employeeId);
        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);
    }
}
