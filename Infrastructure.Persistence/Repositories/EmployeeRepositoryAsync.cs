using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EmployeeRepositoryAsync : GenericRepositoryAsync<Employee>, IEmployeeRepositoryAsync
    {
        private readonly DbSet<Employee> _employees;

        public EmployeeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _employees = dbContext.Set<Employee>();
        }

        public Task<bool> IsUniqueEmailAsync(string email)
        {
            return _employees
                .AllAsync(p => p.Email != email);
        }
    }
}
