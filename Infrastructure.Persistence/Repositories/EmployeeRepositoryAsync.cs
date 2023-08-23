using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class EmployeeRepositoryAsync : IEmployeeRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            await _dbContext.Employees.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsync(Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees
                .ToListAsync();
            }

            return await _dbContext.Employees
                .Where(predicate)
                .ToListAsync();
        }

        public Task<bool> IsUniqueEmailAsync(string email)
        {
            return _dbContext.Employees
                .AllAsync(p => p.Email != email);
        }
    }
}
