
using AutoMapper;
using Shop.BLL.Common.Attachements;
using Shop.BLL.Services.Dtos.Employee;
using Shop.DAL.Entites.Employee;
using Shop.DAL.Preasistince.Repository.EmployeeRepository;
using Shop.DAL.Preasistince.UnitOfWork;

namespace Shop.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IAttachement _attachement;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfwork unitOfwork,
            IAttachement attachement,
            IMapper mapper)
        {
           _unitOfwork = unitOfwork;
            _attachement = attachement;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetEmployee(string search)
        {
            var Employees = _unitOfwork.EmployeeRepository.GetAll().
                Where(E=>!E.IsDeleted&&(string.IsNullOrEmpty(search)||E.Name.ToLower().Trim().Contains(search.ToLower().Trim())
                ||E.Email.ToLower().Trim().Contains(search.ToLower().Trim())))
                .Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Gender=E.Gender.ToString(),
                EmployeeType = E.EmployeeType.ToString(),
                Departrment=E.Department?.Name??"No Department",
                IsActive=E.IsActive,

            });
            return Employees;   
            
        }


        public EmployeeDetailsDto GetEmployeeById(int? id)
        {
            if (id == null)
                return null;
            var Employee= _unitOfwork.EmployeeRepository.GetById(id.Value);
            if (Employee == null)
                return null;
            return new EmployeeDetailsDto()
            {
                Id=Employee.Id,
                Name=Employee.Name,
                Salary=Employee.Salary,
                Address=Employee.Address,
                Age=Employee.Age,
                CreatedBy=1,
                CreatedOn=DateTime.UtcNow,
                DepartmentID=Employee.DepartmentID,
                DepartmentName = Employee.Department?.Name ?? "NoDepartment",
                Email =Employee.Email,
                EmployeeType=Employee.EmployeeType,
                LastModifiedBy=1,
                Gender=Employee.Gender,
                IsActive=Employee.IsActive,
                HiringDate=Employee.HiringDate,
                LastModifiedOn=DateTime.UtcNow,
                PhoneNumber=Employee.PhoneNumber,
                Image=Employee.Image,
               
                
            };
        }

        public int Creat(CreateUpdateEmployeeDto employeeDto)
        {
            #region ManualMapping
            //var Employee = new Employee()
            //{
            //    
            //    Name = employeeDto.Name,
            //    Address = employeeDto.Address,
            //    Salary = employeeDto.Salary,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Email = employeeDto.Email,
            //    Age = employeeDto.Age,
            //    Gender = employeeDto.Gender,
            //    EmployeeType = employeeDto.EmployeeType,
            //    HiringDate = employeeDto.HiringDate,
            //    IsActive = employeeDto.IsActive,
            //    IsDeleted = false,
            //    CreatedBy = 1,
            //    CreatedOn = DateTime.UtcNow,
            //    LastModifiedBy = 1,
            //    LastModifiedOn = employeeDto.LastModifiedOn,
            //    DepartmentID = employeeDto.DepartmentId,
            //    Image=employeeDto.Image,
            //};
            //Employee.Image = _attachement.Uplod(employeeDto.ImageFile, "Iamge");
            //return _employee.Add(Employee); 
            #endregion
            var MappedEmployee=_mapper.Map<CreateUpdateEmployeeDto,Employee>(employeeDto);
            MappedEmployee.Image = _attachement.Uplod(employeeDto.ImageFile, "Image");
             _unitOfwork.EmployeeRepository.Add(MappedEmployee);
           return _unitOfwork.Complete();
        }

        public int Update(CreateUpdateEmployeeDto employeeDto)
        {

            var Employee = new Employee()
            {
                Id = employeeDto.Id,
                CreatedOn= DateTime.UtcNow,
                IsDeleted= false,
                Name = employeeDto.Name,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                PhoneNumber = employeeDto.PhoneNumber,
                Email = employeeDto.Email,
                Age = employeeDto.Age,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                LastModifiedBy = 1,
                CreatedBy = 1,
                HiringDate = employeeDto.HiringDate,
                LastModifiedOn = employeeDto.LastModifiedOn,
                IsActive = employeeDto.IsActive,
                DepartmentID = employeeDto.DepartmentId
            };
            Employee.Image = _attachement.Uplod(employeeDto.ImageFile, "Iamge");
             _unitOfwork.EmployeeRepository.Update(Employee);
            return _unitOfwork.Complete();

        }

        public bool Delete(int? Id)
        {
            var Employee= _unitOfwork.EmployeeRepository.GetById(Id.Value);
            if (Employee != null)
            _unitOfwork.EmployeeRepository.Delete(Employee);
            return _unitOfwork.Complete() > 0;
        }

    }
}
