using Castle.Components.DictionaryAdapter.Xml;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitTests.Employees;

namespace UnitTests.Mocks
{
    public static class MockApplicationDbContext
    {
        public static Mock<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "ApplicationDb")
                    .Options;

            var employeeLists = new List<Employee>
            {
                new Employee{ Id = 1, Name = "Haseeb", Email = "haseeb@email.com", Phone = "0336-0000000", Created = DateTime.UtcNow, DateOfBirth = new DateTime(1998,8, 8), Department = "IT"},
                new Employee{ Id = 2, Name = "Hamid", Email = "hamid@email.com", Phone = "0336-0000001", Created = DateTime.UtcNow, DateOfBirth = new DateTime(1996,8, 8), Department = "Sales"},
            };
            var mockDbContext = new Mock<ApplicationDbContext>(options);

            mockDbContext.Setup(p => p.Employees).Returns(MockDbSet.GetQueryableMockDbSet<Employee>(employeeLists).Object);

            return mockDbContext;
        }
    }
}
