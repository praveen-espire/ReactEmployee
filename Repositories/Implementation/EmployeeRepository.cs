using CVX.DLANGL.DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using Repositories.DBEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IUnitOfWork unitOfWork, ILogger<EmployeeRepository> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            _logger.LogDebug("GetAllEmployees Method Started");

            using (var context = _unitOfWork.GetDBContext())
            {
                var result = default(IList<Employee>);
                result = await _unitOfWork._commonRepository.GetAllAsync<Employee>(null, prop => prop.Role);
                return result;
            }
        }
        public async Task<Employee> GetEmployeeByID(long EmployeeId)
        {
            _logger.LogDebug("GetEmployeeByID Method Started");

            using (TestDbContext context = _unitOfWork.GetDBContext())
            {
                Employee result = await _unitOfWork._commonRepository.FindAsync<Employee>(prop => prop.EmployeeId == EmployeeId);
                return result;
            }
        }
        public async Task<bool> UpdateEmployee(Employee Employee)
        {
            _logger.LogDebug("UpdateEmployee Method Started");

            using (TestDbContext context = _unitOfWork.GetDBContext())
            {
                bool isSuccess = await _unitOfWork._commonRepository.UpdateEntityAsync(Employee);
                return isSuccess;
            }
        }
        public async Task<bool> InsertEmployee(Employee Employee)
        {
            _logger.LogDebug("InsertEmployee Method Started");
            using (TestDbContext context = _unitOfWork.GetDBContext())
            {
                using (var transaction = _unitOfWork.StartTransaction(context))
                {
                    bool isSuccess = await _unitOfWork._commonRepository.InsertEntityAsync(Employee);
                    await transaction.CommitAsync();
                    return isSuccess;
                }
            }
        }
        public async Task<bool> DeleteEmployeeByID(long EmployeeId)
        {
            _logger.LogDebug("GetEmployeeByID Method Started");

            using (TestDbContext context = _unitOfWork.GetDBContext())
            {
                Employee emp = await _unitOfWork._commonRepository.FindAsync<Employee>(prop => prop.EmployeeId == EmployeeId);
                if (emp != null)
                {
                    await _unitOfWork._commonRepository.RemoveEntityAsync<Employee>(emp);
                    return true;
                }
            }
            return false;
        }
    }
}
