using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<Response<IEnumerable<GetAllEmployeesViewModel>>>
    {
        public string KeyWord { get; set; }
    }
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Response<IEnumerable<GetAllEmployeesViewModel>>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeesQueryHandler(IEmployeeRepositoryAsync employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllEmployeesViewModel>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllEmployeesParameter>(request);
            var query = _employeeRepository.GetQueryable();
            if (!string.IsNullOrEmpty(validFilter.KeyWord))
            {
                query = query.Where(x => x.Name.Contains(validFilter.KeyWord) ||
                x.Email.Contains(validFilter.KeyWord) ||
                x.Department.Contains(validFilter.KeyWord));
            }
            var Employee = await query.ToListAsync();
            var employeeViewModel = _mapper.Map<IEnumerable<GetAllEmployeesViewModel>>(Employee);
            return new Response<IEnumerable<GetAllEmployeesViewModel>>(employeeViewModel);           
        }
    }
}
