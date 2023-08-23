using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.CreateEmployee
{
    public partial class CreateEmployeeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;
        public CreateEmployeeCommandHandler(IEmployeeRepositoryAsync employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var Employee = _mapper.Map<Employee>(request);
            await _employeeRepository.AddAsync(Employee);
            return new Response<int>(Employee.Id);
        }
    }
}
