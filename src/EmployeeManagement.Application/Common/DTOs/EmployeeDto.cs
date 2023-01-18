namespace EmployeeManagement.Application.Common.DTOs
{
    public sealed class EmployeeDto
    {
        public int Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Title { get; init; }
        public string? Email { get; init; }
        public string? Address { get; init; }
    }
}
