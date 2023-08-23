using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IEmployeeRepositoryAsync
    {
        Task<Employee> GetByIdAsync(int id);
        Task<IReadOnlyList<Employee>> GetAllAsync(Expression<Func<Employee, bool>> predicate = null);
        Task<Employee> AddAsync(Employee entity);
        Task UpdateAsync(Employee entity);
        Task DeleteAsync(Employee entity);
        Task<bool> IsUniqueEmailAsync(string email);
    }
}
