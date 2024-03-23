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

        }

        public int Add(T entity)
        {
            _dbCcontext.Set<T>().Add(entity);
            return _dbCcontext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbCcontext.Set<T>().Update(entity);
            return _dbCcontext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbCcontext.Set<T>().Remove(entity);
            return _dbCcontext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbCcontext.Set<T>().Find(id);

        }

        public IEnumerable<T> GetAll()
        {
            return _dbCcontext.Set<T>().AsNoTracking().ToList();
        }
    }
}