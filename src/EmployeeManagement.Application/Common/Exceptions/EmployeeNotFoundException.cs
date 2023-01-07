using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Exceptions
{
    public sealed class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(int id) : base($"The {nameof(Employee)} with id: {id} doesn't exist in the database.") 
        { 
        }
    }
}
