using System.Threading.Tasks;
using System.Threading;
using Xunit;
using UnitTests.Mocks;
using Infrastructure.Persistence.Repositories;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Moq;
using Domain.Entities;
using Application.Features.Employees.Commands.UpdateEmployee;
using Application.Features.Employees.Commands.DeleteEmployeeById;
using Application.Exceptions;

namespace UnitTests.Employees.Commands
{
    public class DeleteEmployeeByIdCommandHandlerTests
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly Mock<ApplicationDbContext> _mockDbContext;

        public DeleteEmployeeByIdCommandHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
            _employeeRepository = new EmployeeRepositoryAsync(_mockDbContext.Object);
        }

        [Fact]
        public async Task DeleteEmployeeByIdTests()
        {
            var handler = new DeleteEmployeeByIdCommandHandler(_employeeRepository);
            var result = await handler.Handle(new DeleteEmployeeByIdCommand() { Id = 1 }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            _mockDbContext.Verify(m => m.Employees.Remove(It.IsAny<Employee>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeeByIdTests_ShouldThrowException()
        {
            var handler = new DeleteEmployeeByIdCommandHandler(_employeeRepository);

            ApiException ex = await Assert.ThrowsAsync<ApiException>(() => handler.Handle(new DeleteEmployeeByIdCommand() { Id = 4 }, CancellationToken.None));

            Assert.NotNull(ex);
            Assert.Equal("Employee Not Found.", ex.Message);
        }
    }
}