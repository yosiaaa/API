namespace API.ViewModels.Accounts
{
    public class AccountVM
    {
        public Guid? Guid { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
