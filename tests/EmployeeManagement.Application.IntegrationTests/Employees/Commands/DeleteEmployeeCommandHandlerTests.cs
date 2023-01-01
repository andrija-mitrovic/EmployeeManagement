using EmployeeManagement.API.Services;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using EmployeeManagement.Infrastructure.Persistence.Interceptors;
using EmployeeManagement.Infrastructure.Persistence.Repositories;
using EmployeeManagement.Infrastructure.Services;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.IntegrationTests.Employees.Commands
{
    public sealed class DeleteEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        public DeleteEmployeeCommandHandlerTests()
        {
            var configuration = ReturnConfiguration();
            var optionsBuilder = ReturnDbOptionsBuilder(configuration);
            var currentUserService = new CurrentUserService(new HttpContextAccessor());
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, new DateTimeService());
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options, interceptor);
            var serviceProvider = ReturnServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _employeeRepository = new EmployeeRepository(applicationDbContext);
            _unitOfWork = new UnitOfWork(applicationDbContext);
            _logger = loggerFactory!.CreateLogger<DeleteEmployeeCommandHandler>();
        }

        private static IConfigurationRoot ReturnConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile(Constants.APPSETTINGS_FILE, true, true).Build();
        }

        private static DbContextOptionsBuilder<ApplicationDbContext> ReturnDbOptionsBuilder(IConfigurationRoot configuration)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString(Constants.CONNECTION_STRING_NAME));
        }

        private static ServiceProvider ReturnServiceProvider()
        {
            return new ServiceCollection().AddLogging().BuildServiceProvider();
        }

        [Fact]
        public async Task Should_DeleteEmployee_WhenDeleteRequestIsValid()
        {
            var command = await ReturnDeleteEmployeeCommand();

            var handler = new DeleteEmployeeCommandHandler(
                _employeeRepository,
                _unitOfWork,
                _logger);

            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().Be(Unit.Value);
        }

        private async Task<DeleteEmployeeCommand> ReturnDeleteEmployeeCommand()
        {
            var employee = ReturnEmployee();

            return new() 
            { 
                Id = await _employeeRepository.GetEmployeeId(employee) ?? 0
            };
        }

        private static Employee ReturnEmployee()
        {
            return new()
            {
                FirstName = "Andrija",
                LastName = "Mitrovic",
                Title = "Software engineer",
                Address = "Herceg Novi",
                Email = "andrija@gmail.com"
            };
        }
    }
}
