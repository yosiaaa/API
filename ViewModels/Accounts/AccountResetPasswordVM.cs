using API.Models;
using API.ViewModels.Accounts;

namespace API.ViewModels.Accounts
{
    public class AccountResetPasswordVM
    {
        public int OTP { get; set; }
        public string Email { get; set; }
    }
}
