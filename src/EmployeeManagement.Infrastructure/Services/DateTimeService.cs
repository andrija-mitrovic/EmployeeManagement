using EmployeeManagement.Application.Common.Interfaces;

namespace EmployeeManagement.Infrastructure.Services
{
    internal sealed class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
