using Repositories.DBEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeByID(long EmployeeId);
        Task<bool> UpdateEmployee(Employee Employee);
        Task<bool> InsertEmployee(Employee Employee);
        Task<bool> DeleteEmployeeByID(long EmployeeId);
    }
}
