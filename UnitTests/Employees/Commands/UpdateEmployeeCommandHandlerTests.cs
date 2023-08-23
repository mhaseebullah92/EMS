using System.Threading.Tasks;
using System.Threading;
using Xunit;
using UnitTests.Mocks;
using Infrastructure.Persistence.Repositories;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using System;
using Moq;
using Domain.Entities;
using Application.Features.Employees.Commands.UpdateEmployee;
using Application.Exceptions;
using Application.Features.Employees.Queries.GetEmployeeById;

namespace UnitTests.Employees.Commands
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly Mock<ApplicationDbContext> _mockDbContext;

        public UpdateEmployeeCommandHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
            _employeeRepository = new EmployeeRepositoryAsync(_mockDbContext.Object);
        }

        [Fact]
        public async Task UpdateEmployeeTests()
        {
            var handler = new UpdateEmployeeCommandHandler(_employeeRepository);
            var result = await handler.Handle(new UpdateEmployeeCommand() { Id = 1, Name = "Haseebu", Email = "haseebu@email.com", Phone = "0336-0000001", DateOfBirth = new DateTime(1998, 1, 8), Department = "IT" }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            _mockDbContext.Verify(m => m.Employees.Update(It.IsAny<Employee>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeTests_ShouldThrowException()
        {
            var handler = new UpdateEmployeeCommandHandler(_employeeRepository);

            ApiException ex = await Assert.ThrowsAsync<ApiException>(() => handler.Handle(new UpdateEmployeeCommand() { Id = 4, Name = "Haseebu", Email = "haseebu@email.com", Phone = "0336-0000001", DateOfBirth = new DateTime(1998, 1, 8), Department = "IT" }, CancellationToken.None));

            Assert.NotNull(ex);
            Assert.Equal("Employee Not Found.", ex.Message);
        }
    }
}