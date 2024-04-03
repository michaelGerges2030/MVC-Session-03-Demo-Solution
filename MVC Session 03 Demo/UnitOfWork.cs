using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Data.Configurations;
using Route.C41.G03.DAL.Models;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace MVC_Session_03_Demo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories; 

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();    
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;   
            if (!_repositories.ContainsKey(key)) 
            {
          
                if (key == nameof(Employee)) 
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositories.Add(key, repository);
                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(key, repository);
                }
            }
           return _repositories[key] as IGenericRepository<T>;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
