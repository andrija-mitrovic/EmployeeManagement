using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; }
    }
}
