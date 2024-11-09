
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Entites;
using Shop.DAL.Preasistince.Data.Contexts;

namespace Shop.DAL.Preasistince.Repository._GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ShopDbContext _context;

        public GenericRepository(ShopDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        =>_context.Set<T>().AsNoTracking().ToList();

        public T? GetById(int id)
        =>_context.Set<T>().Find(id);


        public void Add(T entity)
         =>  _context.Set<T>().Add(entity);
          


        public void Update(T entity)
        =>
            _context.Set<T>().Update(entity);
       

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
           
        }

    }
}
