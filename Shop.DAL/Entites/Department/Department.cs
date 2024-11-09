

using Shop.DAL.Entites.Employee;
using System.ComponentModel.DataAnnotations;

namespace Shop.DAL.Entites.Department
{
    public class Department:BaseEntity
    {
        [Required(ErrorMessage="Name Requierd Ya Hamada")]

        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code Requierd Ya Hamada")]

        public int Code { get; set; }

        public string? Description { get; set; }
        public virtual ICollection<Employee.Employee> Employees { get; set; }=new HashSet<Employee.Employee>();
    }
}
