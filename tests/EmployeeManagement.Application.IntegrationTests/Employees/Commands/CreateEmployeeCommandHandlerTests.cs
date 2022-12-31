using AutoMapper;
using EmployeeManagement.API.Services;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Mappings;
using EmployeeManagement.Application.Employees.Commands.CreateEmployee;
using EmployeeManagement.Infrastructure.Persistence;
using EmployeeManagement.Infrastructure.Persistence.Interceptors;
using EmployeeManagement.Infrastructure.Persistence.Repositories;
using EmployeeManagement.Infrastructure.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.IntegrationTests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;

        public CreateEmployeeCommandHandlerTests()
        {
            var configuration = ReturnConfiguration();
            var optionsBuilder = ReturnDbOptionsBuilder(configuration);
            var currentUserService = new CurrentUserService(new HttpContextAccessor());
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, new DateTimeService());
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options, interceptor);
            var mapperConfiguration = ReturnMapperConfiguration();
            var serviceProvider = ReturnServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _employeeRepository = new EmployeeRepository(applicationDbContext);
            _unitOfWork = new UnitOfWork(applicationDbContext);
            _mapper = mapperConfiguration.CreateMapper();
            _logger = loggerFactory!.CreateLogger<CreateEmployeeCommandHandler>();
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

        private static MapperConfiguration ReturnMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
        }

        private static ServiceProvider ReturnServiceProvider()
        {
            return new ServiceCollection().AddLogging().BuildServiceProvider();
        }

        [Fact]
        public async Task Handle_ShouldCreateEmployee_WhenCreateRequestIsValid()
        {
            var command = ReturnCreateEmployeeCommand();

            var handler = new CreateEmployeeCommandHandler(
                _employeeRepository,
                _unitOfWork,
                _mapper,
                _logger);

            var result = await handler.Handle(command, default);

            result.Should().BePositive();
            result.Should().NotBe(null);
        }

        private static CreateEmployeeCommand ReturnCreateEmployeeCommand() =>
            new()
            {
                FirstName = "Andrija",
                LastName = "Mitrovic",
                Title = "Software engineer",
                Address = "Herceg Novi",
                Email = "andrija@gmail.com"
            };
    }
}