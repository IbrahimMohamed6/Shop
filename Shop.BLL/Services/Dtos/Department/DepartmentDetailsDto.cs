using System.ComponentModel.DataAnnotations;

namespace Shop.BLL.Services.Dtos.Department
{
    public class DepartmentDetailsDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Requierd Ya Hamada")]

        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code Requierd Ya Hamada")]

        public int Code { get; set; }

        public string? Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}
