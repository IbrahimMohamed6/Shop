using Shop.DAL.Entites.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services.Dtos.Employee
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        
        public int? Age { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HiringDate { get; set; }
        public bool IsActive { get; set; }
        // New Properties
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedOn { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentID { get; set; }  // Nullable to handle the case when it's null
        public string? DepartmentName { get; set; }  // Name of the department (nullable)
        public string? Image { get; set; }

    }
}
