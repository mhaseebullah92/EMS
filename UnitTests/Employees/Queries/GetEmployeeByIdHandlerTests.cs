using System.Threading.Tasks;
using System.Threading;
using Xunit;
using UnitTests.Mocks;
using Infrastructure.Persistence.Repositories;
using Application.Features.Employees.Queries.GetEmployeeById;
using Application.Interfaces.Repositories;
using System;
using Application.Exceptions;

namespace UnitTests.Employees.Queries
{
    public class GetEmployeeByIdHandlerTests
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;

        public GetEmployeeByIdHandlerTests()
        {
            _employeeRepository = new EmployeeRepositoryAsync(MockApplicationDbContext.GetDbContext().Object);
        }

        [Fact]
        public async Task GetEmployeeByIdTests_ShouldReturn()
        {
            var handler = new GetEmployeeByIdQueryHandler(_employeeRepository);
            var result = await handler.Handle(new GetEmployeeByIdQuery() { Id = 1 }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.Equal("haseeb@email.com", result.Data.Email);
        }

        [Fact]
        public async Task GetEmployeeByIdTests_ShouldThrowException()
        {
            var handler = new GetEmployeeByIdQueryHandler(_employeeRepository);

            ApiException ex = await Assert.ThrowsAsync<ApiException>(() => handler.Handle(new GetEmployeeByIdQuery() { Id = 3 }, CancellationToken.None));

            Assert.NotNull(ex);
            Assert.Equal("Employee Not Found.", ex.Message);
        }
    }
}
