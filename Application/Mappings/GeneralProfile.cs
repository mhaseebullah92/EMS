using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Queries.GetAllEmployees;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Employee, GetAllEmployeesViewModel>().ReverseMap();
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<GetAllEmployeesQuery, GetAllEmployeesParameter>();
        }
    }
}
