using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
