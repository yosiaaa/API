using API.Models;
using API.ViewModels.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        int CreateWithValidate(Employee employee);

        //kel 1
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        //kel 5 & 6
        Guid? FindGuidByEmail(string email);

        bool CheckEmailAndPhoneAndNik(string value);
    }
}
