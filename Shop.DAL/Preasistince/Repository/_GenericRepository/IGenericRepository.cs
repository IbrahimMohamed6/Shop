using Shop.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Preasistince.Repository._GenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> GetAll();
        T? GetById(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);    
    }
}
