using AutoMapper;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeeManagement.Application.UnitTests.Employees.Commands
{
    public sealed class UpdateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UpdateEmployeeCommandHandler>> _loggerMock;
        private readonly UpdateEmployeeCommandHandler _handler;

        public UpdateEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapperMock = new();
            _loggerMock = new();

            _handler = new UpdateEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateEmployee_WhenUpdateRequestIsValid()
        {
            var command = ReturnUpdateEmployeeCommand();
            var employee = ReturnEmployee();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(employee);

            var result = await _handler.Handle(command, default);

            result.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUpdateRequestIsNotValid()
        {
            var command = ReturnUpdateEmployeeCommand();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(value: null);

            Func<Task> result = async () => await _handler.Handle(command, default);

            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository_WhenUpdateRequestIsValid()
        {
            var command = ReturnUpdateEmployeeCommand();
            var employee = ReturnEmployee();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(employee);

            var result = await _handler.Handle(command, default);

            _employeeRepositoryMock.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallUnitOfWork_WhenUpdateRequestIsValid()
        {
            var command = ReturnUpdateEmployeeCommand();
            var employee = ReturnEmployee();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(employee);

            var result = await _handler.Handle(command, default);

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
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

        private static UpdateEmployeeCommand ReturnUpdateEmployeeCommand() =>
            new()
            {
                Id = 1,
                FirstName = "Andrija",
                LastName = "Mitrovic",
                Title = "Software engineer",
                Address = "Herceg Novi",
                Email = "andrija@gmail.com"
            };
    }
}
