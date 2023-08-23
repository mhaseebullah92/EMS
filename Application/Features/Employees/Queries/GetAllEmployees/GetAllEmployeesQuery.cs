using Application.Filters;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<PagedResponse<IEnumerable<GetAllEmployeesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, PagedResponse<IEnumerable<GetAllEmployeesViewModel>>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeesQueryHandler(IEmployeeRepositoryAsync employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllEmployeesViewModel>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllEmployeesParameter>(request);
            var Employee = await _employeeRepository.GetPagedReponseAsync(validFilter.PageNumber,validFilter.PageSize);
            var employeeViewModel = _mapper.Map<IEnumerable<GetAllEmployeesViewModel>>(Employee);
            return new PagedResponse<IEnumerable<GetAllEmployeesViewModel>>(employeeViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
