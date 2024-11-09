
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Entites.Department;
using Shop.DAL.Preasistince.Data.Contexts;
using Shop.DAL.Preasistince.Repository._GenericRepository;

namespace Shop.DAL.Preasistince.Repository.DepartmentRepositories
{
    public class DepartmentRepository:GenericRepository<Department>,IDepartmentRepository
    {
        private readonly ShopDbContext _dbContext;

        public DepartmentRepository(ShopDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
