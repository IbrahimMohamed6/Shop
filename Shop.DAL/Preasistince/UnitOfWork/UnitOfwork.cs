using Shop.DAL.Preasistince.Data.Contexts;
using Shop.DAL.Preasistince.Repository.DepartmentRepositories;
using Shop.DAL.Preasistince.Repository.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Preasistince.UnitOfWork
{
    public class UnitOfwork : IUnitOfwork
    {
        private readonly ShopDbContext _dbContext;

        public UnitOfwork(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public IEmployeeRepository EmployeeRepository { get
            {
                return new EmployeeRepisitory(_dbContext);
            }
        }
        public IDepartmentRepository DepartmentRepository { get 
            { 
                return new DepartmentRepository(_dbContext);
            }
        }

        public int Complete()
      => _dbContext.SaveChanges();

        public void Dispose()
            =>_dbContext.Dispose();
       
    }
}
