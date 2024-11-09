using Shop.DAL.Entites.Department;
using Shop.DAL.Entites.Helper;
using System.ComponentModel.DataAnnotations;

namespace Shop.DAL.Entites.Employee
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Salary { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
       
        public string Email { get; set; } = null!;
        public int? Age { get; set; }
        public DateTime HiringDate { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public virtual Department.Department? Department { get; set; }
        public int? DepartmentID { get; set; }
        public string? Image { get; set; }

    }
}
