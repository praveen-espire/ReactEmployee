using AutoMapper;
using Entities.BizEntities;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.DBEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEmployeeRepository _employeeRepository;
        public IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;


        public EmployeeService(IEmployeeRepository EmployeeRepository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepository = EmployeeRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<EmployeeBizEntity>> GetAllEmployees()
        {
            _logger.LogDebug("GetAllEmployees Method Started!");

            var employees = default(List<EmployeeBizEntity>);
            var employeeData = await _employeeRepository.GetAllEmployees();
            if (employeeData != null && employeeData.Any())
            {
                employees = new List<EmployeeBizEntity>();
                employeeData.ToList().ForEach(z =>
                {
                    var mappedEmployee = _mapper.Map<EmployeeBizEntity>(z);
                    mappedEmployee.RoleName = mappedEmployee.Role?.RoleName;
                    employees.Add(mappedEmployee);
                });
            }
            return employees;
        }
        public async Task<EmployeeBizEntity> GetEmployeeByID(long EmployeeId)
        {
            _logger.LogDebug("GetEmployeeByID Method Started!");

            var employeeBizEntity = default(EmployeeBizEntity);
            var employee = await _employeeRepository.GetEmployeeByID(EmployeeId);
            if (employee != null)
            {
                employeeBizEntity = _mapper.Map<EmployeeBizEntity>(employee);
                employeeBizEntity.RoleName = employeeBizEntity.Role?.RoleName;

            }
            return employeeBizEntity;
        }
        public async Task<string> UpdateEmployee(EmployeeBizEntity EmployeeBizEntity)
        {
            _logger.LogDebug("UpdateEmployee Method Started!");

            var employee = await _employeeRepository.GetEmployeeByID(EmployeeBizEntity.EmployeeId);
            if (employee != null)
            {
                employee.EmployeeNumber = EmployeeBizEntity.EmployeeNumber;
                employee.FirstName = EmployeeBizEntity.FirstName;
                employee.LastName = EmployeeBizEntity.LastName;
                employee.DateJoined = EmployeeBizEntity.DateJoined;
                employee.Extension = EmployeeBizEntity.Extension;
                employee.RoleId = EmployeeBizEntity.RoleId;

                var result = await _employeeRepository.UpdateEmployee(employee);
                if (result)
                {
                    return "Success";
                }
            }
            return null;
        }
        public async Task<string> InsertEmployee(EmployeeBizEntity EmployeeBizEntities)
        {
            _logger.LogDebug("InsertEmployee Method Started!");

            var employeeDbEntity = _mapper.Map<Employee>(EmployeeBizEntities);
            var result = await _employeeRepository.InsertEmployee(employeeDbEntity);
            if (result)
            {
                return "Success";
            }
            return null;
        }
        public async Task<bool> DeleteEmployeeByID(long EmployeeId)
        {
            _logger.LogDebug("DeleteEmployeeByID Method Started!");
            return await _employeeRepository.DeleteEmployeeByID(EmployeeId);
        }
    }
}
