using AutoMapper;
using Shop.BLL.Services.Dtos.Employee;
using Shop.DAL.Entites.Employee;


namespace Shop.BLL.Mapping
{
    public class MappingProfils:Profile
    {
        public MappingProfils()
        {
            #region Employee
            CreateMap<Employee, CreateUpdateEmployeeDto>().ReverseMap();
            #endregion
        }
    }
}
