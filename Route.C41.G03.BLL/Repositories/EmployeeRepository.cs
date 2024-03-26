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
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context): base(context)
        {
            
        }

        public IQueryable<Employee> GetEmployeeByAddress(string address)
            =>_dbCcontext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
        

        public IQueryable<Employee> SearchByName(string name)
            => _dbCcontext.Employees.Where(E => E.Name.ToLower().Contains(name));
    }
}
