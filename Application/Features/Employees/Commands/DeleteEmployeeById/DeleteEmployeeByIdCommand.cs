using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.DeleteEmployeeById
{
    public class DeleteEmployeeByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, Response<int>>
        {
            private readonly IEmployeeRepositoryAsync _employeeRepository;
            public DeleteEmployeeByIdCommandHandler(IEmployeeRepositoryAsync employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Response<int>> Handle(DeleteEmployeeByIdCommand command, CancellationToken cancellationToken)
            {
                var Employee = await _employeeRepository.GetByIdAsync(command.Id);
                if (Employee == null) throw new ApiException($"Employee Not Found.");
                await _employeeRepository.DeleteAsync(Employee);
                return new Response<int>(Employee.Id);
            }
        }
    }
}
