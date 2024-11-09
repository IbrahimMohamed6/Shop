using System.ComponentModel.DataAnnotations;

namespace Shop.BLL.Services.Dtos.Department
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        
        public int Code { get; set; }

        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }
    }
}
