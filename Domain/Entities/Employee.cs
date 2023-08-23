using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Employee : AuditableBaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(250)]
        public string Department { get; set; }
    }
}
