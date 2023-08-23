using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<Response<Employee>>
    {
        public int Id { get; set; }

        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Response<Employee>>
        {
            private readonly IEmployeeRepositoryAsync _employeeRepository;
            public GetEmployeeByIdQueryHandler(IEmployeeRepositoryAsync employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Response<Employee>> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
            {
                var Employee = await _employeeRepository.GetByIdAsync(query.Id);
                if (Employee == null) throw new ApiException($"Employee Not Found.");
                return new Response<Employee>(Employee);
            }
        }
    }
}
