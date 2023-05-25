using API.Models;
using API.ViewModels.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        // Kelompok 2
        int CreateWithValidate(Employee employee);

        // Kelompok 5
        Guid? FindGuidByEmail(string email);
    }
}
