using Entities.BizEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeBizEntity>> GetAllEmployees();
        Task<EmployeeBizEntity> GetEmployeeByID(long EmployeeId);
        Task<string> UpdateEmployee(EmployeeBizEntity EmployeeBizEntity);
        Task<string> InsertEmployee(EmployeeBizEntity EmployeeBizEntities);
        Task<bool> DeleteEmployeeByID(long EmployeeId);
    }
}
