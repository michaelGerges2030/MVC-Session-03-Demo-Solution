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
        {
            return _dbCcontext.Employees.Where(e => e.Address.ToLower() == address.ToLower());
        }
    }
}
