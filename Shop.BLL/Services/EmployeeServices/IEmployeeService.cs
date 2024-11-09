
using Shop.BLL.Services.Dtos.Department;
using Shop.BLL.Services.Dtos.Employee;

namespace Shop.BLL.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployee(string search);
        EmployeeDetailsDto GetEmployeeById(int? id);

        int Creat(CreateUpdateEmployeeDto employeeDto);
        int Update(CreateUpdateEmployeeDto employeeDto);
        bool Delete(int? Id);
    }
}
