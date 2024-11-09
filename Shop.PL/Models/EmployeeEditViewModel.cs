using Shop.DAL.Entites.Helper;
using System.ComponentModel.DataAnnotations;

namespace Shop.PL.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Name Is Requierd Ya Hamada")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Adderess Is Requierd Ya Hamada")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Salary Is Requierd Ya Hamada")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Is Requierd Ya Hamada")]
        public string Email { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }
        [Range(18, 60)]
        public int? Age { get; set; }

        // New Properties (usually handled automatically or through service logic)
        public int CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int LastModifiedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public string Gender { get; set; }= null!;
        public string EmployeeType { get; set; }     = null!;
        public int? DepartmentId { get; set; }
    }
}
