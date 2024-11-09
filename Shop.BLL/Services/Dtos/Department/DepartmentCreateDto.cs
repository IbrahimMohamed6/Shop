using System.ComponentModel.DataAnnotations;

namespace Shop.BLL.Services.Dtos.Department
{
    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "Name Is Requierd Ya Hamada")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code Is Requierd Ya Hamada")]
        public int Code { get; set; }

        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }
    }
}
