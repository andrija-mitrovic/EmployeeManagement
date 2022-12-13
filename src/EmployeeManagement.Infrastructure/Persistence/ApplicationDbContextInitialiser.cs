using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Infrastructure.Persistence
{
    public sealed class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;

        public ApplicationDbContextInitialiser(
            ApplicationDbContext context, 
            ILogger<ApplicationDbContextInitialiser> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (_context.Employees.Any()) return;

            List<Employee> products = GetEmployees();

            await _context.Employees.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }

        private static List<Employee> GetEmployees() => 
            new()
            {
                new Employee
                {
                    FirstName = "Andrija",
                    LastName = "Mitrovic",
                    Title = "Software engineer",
                    Email = "andrija@gmail.com",
                    Address = "Herceg Novi"
                },
                new Employee
                {
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Title = "Project manager",
                    Email = "petar@gmail.com",
                    Address = "Podgorica"
                }
            };
    }
}
