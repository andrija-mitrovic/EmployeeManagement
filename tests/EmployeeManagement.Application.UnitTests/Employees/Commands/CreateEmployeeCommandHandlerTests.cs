using AutoMapper;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Employees.Commands.CreateEmployee;
using EmployeeManagement.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeeManagement.Application.UnitTests.Employees.Commands
{
    public sealed class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<CreateEmployeeCommandHandler>> _loggerMock;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapperMock = new();
            _loggerMock = new();

            _handler = new CreateEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateEmployee_WhenCreateRequestIsValid()
        {
            var command = ReturnCreateEmployeeCommand();
            var employee = ReturnEmployee();

            _mapperMock.Setup(x => x.Map<Employee>(command)).Returns(employee);

            var result = await _handler.Handle(command, default);

            result.Should().BePositive();
            result.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WhenCreateRequestIsValid()
        {
            var command = ReturnCreateEmployeeCommand();
            var employee = ReturnEmployee();

            _mapperMock.Setup(x => x.Map<Employee>(command)).Returns(employee);

            var result = await _handler.Handle(command, default);

            _employeeRepositoryMock.Verify(x => x.AddAsync(It.Is<Employee>(y => y.Id == result), default), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallUnitOfWork_WhenCreateRequestIsValid()
        {
            var command = ReturnCreateEmployeeCommand();
            var employee = ReturnEmployee();

            _mapperMock.Setup(x => x.Map<Employee>(command)).Returns(employee);

            var result = await _handler.Handle(command, default);

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        private static Employee ReturnEmployee() =>
            new()
            {
                Id = 1,
                FirstName = "Andrija",
                LastName = "Mitrovic",
                Title = "Software engineer",
                Address = "Herceg Novi",
                Email = "andrija@gmail.com"
            };

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
