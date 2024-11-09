using Shop.DAL.Entites.Employee;
using Shop.DAL.Preasistince.Data.Contexts;
using Shop.DAL.Preasistince.Repository._GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Preasistince.Repository.EmployeeRepository
{
    public class EmployeeRepisitory: GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepisitory(ShopDbContext context):base(context) { }


    }
}
