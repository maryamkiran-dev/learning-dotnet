using CQRS.Model;

namespace CQRS.Services
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAllEmployeesAsync();
        public Task<Employee?> GetEmployeeByIdAsync(int id);
        public Task<Employee> AddEmployeeAsync(Employee employee); 
        public Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);
        public Task<int> DeleteEmployeeByIdAsync(int id);

    }
}
