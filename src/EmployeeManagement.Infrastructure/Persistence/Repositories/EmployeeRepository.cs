using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories
{
    internal sealed class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<int?> GetEmployeeId(Employee employee)
        {
            return (await _dbContext.Employees.FirstOrDefaultAsync(x => x.FirstName == employee.FirstName &&
                                                                        x.LastName == employee.LastName &&
                                                                        x.Title == employee.Title &&
                                                                        x.Address == employee.Address &&
                                                                        x.Email == employee.Email))?.Id;
        }
    }
}
