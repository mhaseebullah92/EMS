using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
        {
            private readonly IEmployeeRepositoryAsync _employeeRepository;
            public UpdateEmployeeCommandHandler(IEmployeeRepositoryAsync employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Response<int>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
            {
                var Employee = await _employeeRepository.GetByIdAsync(command.Id);

                if (Employee == null)
                {
                    throw new ApiException($"Employee Not Found.");
                }
                else
                {
                    Employee.Name = command.Name;
                    Employee.Email = command.Email;
                    Employee.Phone = command.Phone;
                    Employee.DateOfBirth = command.DateOfBirth;
                    Employee.Department = command.Department;
                    await _employeeRepository.UpdateAsync(Employee);
                    return new Response<int>(Employee.Id);
                }
            }
        }
    }
}
