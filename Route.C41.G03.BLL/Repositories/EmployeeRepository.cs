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
            =>_dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Set<Employee>().Include(E => E.Department).AsNoTracking().ToListAsync();
        }

        public IQueryable<Employee> SearchByName(string name)
            => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
    }
}
