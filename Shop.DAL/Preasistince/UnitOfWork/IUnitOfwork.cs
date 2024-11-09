using Shop.DAL.Preasistince.Repository.DepartmentRepositories;
using Shop.DAL.Preasistince.Repository.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Preasistince.UnitOfWork
{
    public interface IUnitOfwork:IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;}
        public IDepartmentRepository  DepartmentRepository { get; }

        int Complete();
    }
}
