
using Shop.BLL.Services.Dtos.Department;
using Shop.DAL.Entites.Department;
using Shop.DAL.Preasistince.Repository.DepartmentRepositories;
using Shop.DAL.Preasistince.UnitOfWork;

namespace Shop.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfwork _unitOfwork;

        public DepartmentServices(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }


        public IEnumerable<DepartmentDto> GetAllDepartment()
        {
            var Department=_unitOfwork.DepartmentRepository.GetAll().Where(D=>!D.IsDeleted).Select(D=>new DepartmentDto
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                CreatedOn = D.CreatedOn,
            }).ToList();
            return Department;
            
        }


        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var Department=_unitOfwork.DepartmentRepository.GetById(id);
            if(Department is not null)
            return new DepartmentDetailsDto()
            {
                Id=Department.Id,
                Name=Department.Name,
                Code=Department.Code,
                Description=Department.Description,
                CreatedOn=Department.CreatedOn,
                CreatedBy=1,
                IsDeleted=Department.IsDeleted,
                LastModifiedBy =1,
                LastModifiedOn =Department.LastModifiedOn,
            };
            return null;

               
            
        }

        public int Creat(DepartmentCreateDto department)
        {
            var Department = new Department()
            {

                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                LastModifiedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedOn = DateTime.UtcNow,
                CreatedBy = 1,
                

            };
             _unitOfwork.DepartmentRepository.Add(Department);
            return _unitOfwork.Complete();
        }

        public int Update(DepartmentUpdateDto department)
        {
            var Deaprtment = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreatedOn = DateTime.UtcNow,
                Description = department.Description,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };
            _unitOfwork.DepartmentRepository.Update(Deaprtment);
            return _unitOfwork.Complete();
        }

        public bool Delete(int Id)
        {
            var DeleteDepartment=_unitOfwork.DepartmentRepository.GetById(Id);
            if(DeleteDepartment != null)
                 _unitOfwork.DepartmentRepository.Delete(DeleteDepartment);
            return _unitOfwork.Complete() > 0;
        }

    }
}
