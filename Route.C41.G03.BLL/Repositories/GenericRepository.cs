using Microsoft.EntityFrameworkCore;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Data.Configurations;
using Route.C41.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G03.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbCcontext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbCcontext = dbContext;
        }

        public void Add(T entity)
            => _dbCcontext.Set<T>().Add(entity);
        

        public void Update(T entity)
           => _dbCcontext.Set<T>().Update(entity);


        public void Delete(T entity)
           => _dbCcontext.Set<T>().Remove(entity);


        public T Get(int id)
            =>_dbCcontext.Set<T>().Find(id);

        public IEnumerable<T> GetAll()
        {
           if (typeof(T) == typeof(Employee)) 
            {
                return (IEnumerable<T>) _dbCcontext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            }
           else
            {
                return _dbCcontext.Set<T>().AsNoTracking().ToList();
            }
        }
    }
}