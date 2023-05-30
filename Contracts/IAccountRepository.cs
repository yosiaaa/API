using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;
using API.ViewModels.Others;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        int Register(RegisterVM registerVM);

        //kel 3
        AccountEmpVM Login(LoginVM loginVM);

        //kel 5
        int UpdateOTP(Guid? employeeId);

        //kel 6
        public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);

        IEnumerable<string> GetRoles(Guid Guid);
        Employee GetByEmail(string email);

    }
}