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
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbCcontext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbCcontext = dbContext;
        }

        public int Add(Employee entity)
        {
            _dbCcontext.Employees.Add(entity);
            return _dbCcontext.SaveChanges();
        }

        public int Update(Employee entity)
        {
            _dbCcontext.Employees.Update(entity);
            return _dbCcontext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbCcontext.Employees.Remove(entity);
            return _dbCcontext.SaveChanges();
        }

        public Employee Get(int id)
        {
            //var Employee = _dbCcontext.Employees.Local.Where( D => D.Id == id).FirstOrDefault();
            //if (Employee == null)
            //	Employee = _dbCcontext.Employees.Where(D => D.Id == id).FirstOrDefault();
            //return Employee;

            //return _dbCcontext.Find<Employee>(id);
            return _dbCcontext.Employees.Find(id);

        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbCcontext.Employees.AsNoTracking().ToList();
        }
    }
}
