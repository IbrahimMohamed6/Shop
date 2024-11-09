

using Shop.DAL.Entites.Helper;

namespace Shop.BLL.Services.Dtos.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public string EmployeeType { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Departrment { get; set; }
    }
}
