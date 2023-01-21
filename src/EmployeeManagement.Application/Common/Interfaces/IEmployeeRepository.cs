using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<int?> GetEmployeeId(Employee employee, bool disableTracking = true, CancellationToken cancellationToken = default);
    }
}
