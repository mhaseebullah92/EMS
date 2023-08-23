using Application.Features.Employees.Commands.CreateEmployee;
using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;

        public CreateEmployeeCommandValidator(IEmployeeRepositoryAsync employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(p => p.Department)
                .NotEmpty().WithMessage("{Department} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Department} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{Email} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Email} must not exceed 50 characters.")
                .MustAsync(IsUniqueEmail).WithMessage("{Email} already exists.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} must not exceed 50 characters.");
                
        }

        private async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _employeeRepository.IsUniqueEmailAsync(email);
        }
    }
}
