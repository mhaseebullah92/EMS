using System.Threading.Tasks;
using System.Threading;
using Xunit;
using UnitTests.Mocks;
using Infrastructure.Persistence.Repositories;
using AutoMapper;
using Moq;
using Application.Mappings;
using Application.Features.Employees.Queries.GetAllEmployees;
using System.Linq;
using Application.Interfaces.Repositories;

namespace UnitTests.Employees.Queries
{
    public class GetAllEmployeesQueryHandlerTests
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandlerTests()
        {
            _employeeRepository = new EmployeeRepositoryAsync(MockApplicationDbContext.GetDbContext().Object);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile(new GeneralProfile())));
        }

        [Fact]
        public async Task GetAllEmployeesTests_ShouldReturnAllEMployees()
        {
            var handler = new GetAllEmployeesQueryHandler(_employeeRepository, _mapper);
            var result = await handler.Handle(new GetAllEmployeesQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetAllEmployeesTests_ShouldReturnOneEMployee()
        {
            var handler = new GetAllEmployeesQueryHandler(_employeeRepository, _mapper);
            var result = await handler.Handle(new GetAllEmployeesQuery() { KeyWord = "Haseeb" }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.Single(result.Data);
        }
    }
}
