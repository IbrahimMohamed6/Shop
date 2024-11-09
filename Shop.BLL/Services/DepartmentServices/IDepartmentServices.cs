
using Shop.BLL.Services.Dtos.Department;
using Shop.DAL.Entites.Department;

namespace Shop.BLL.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentDto> GetAllDepartment();
        DepartmentDetailsDto GetDepartmentById(int id);

        int Creat(DepartmentCreateDto department); 
        int Update(DepartmentUpdateDto department); 
        bool Delete(int Id);

    }
}
