using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeeManagement.Application.UnitTests.Employees.Commands
{
    public sealed class DeleteEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<DeleteEmployeeCommandHandler>> _loggerMock;
        private readonly DeleteEmployeeCommandHandler _handler;

        public DeleteEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new();
            _unitOfWorkMock = new();
            _loggerMock = new();

            _handler = new DeleteEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteEmployee_WhenEmployeeExists()
        {
            var command = ReturnDeleteEmployeeCommand();
            var employee = ReturnEmployee();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(employee);

            var result = await _handler.Handle(command, default);

            result.Should().BeEquivalentTo(Unit.Value);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenEmployeeDoesntExist()
        {
            var command = ReturnDeleteEmployeeCommand();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(value: null);

            Func<Task> result = async () => await _handler.Handle(command, default);

            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteOnRepository_WhenEmployeeExists()
        {
            var command = ReturnDeleteEmployeeCommand();
            var employee = ReturnEmployee();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(employee);

            var result = await _handler.Handle(command, default);

            _employeeRepositoryMock.Verify(x => x.Delete(employee), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallUnitOfWork_WhenEmployeeExists()
        {
            var command = ReturnDeleteEmployeeCommand();
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

        private static DeleteEmployeeCommand ReturnDeleteEmployeeCommand() =>
            new()
            {
                Id = 1
            };
    }
}
