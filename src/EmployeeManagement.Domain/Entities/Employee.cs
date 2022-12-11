using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Domain.Entities
{
    public sealed class Employee : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
