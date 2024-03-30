using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Data.Configurations;
using System;

namespace MVC_Session_03_Demo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        { 
        _dbContext.Dispose();
        }


    }
}
