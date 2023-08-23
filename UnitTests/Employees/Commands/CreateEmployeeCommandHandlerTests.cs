using System.Threading.Tasks;
using System.Threading;
using Xunit;
using UnitTests.Mocks;
using Infrastructure.Persistence.Repositories;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Application.Features.Employees.Commands.CreateEmployee;
using AutoMapper;
using Application.Mappings;
using System;
using Moq;
using Domain.Entities;

namespace UnitTests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
            _employeeRepository = new EmployeeRepositoryAsync(_mockDbContext.Object);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile(new GeneralProfile())));
        }

        [Fact]
        public async Task CreateEmployeeTests()
        {
            var handler = new CreateEmployeeCommandHandler(_employeeRepository, _mapper);
            var result = await handler.Handle(new CreateEmployeeCommand() { Name = "Safi", Email = "safi@email.com", Phone = "0336-0000011", DateOfBirth = new DateTime(1998, 8, 1), Department = "Support" }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            _mockDbContext.Verify(m => m.Employees.AddAsync(It.IsAny<Employee>(), default), Times.Once);
            _mockDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}