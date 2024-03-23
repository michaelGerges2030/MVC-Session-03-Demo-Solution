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
	public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
	{
        public DepartmentRepository(ApplicationDbContext context): base(context)
        {
            
        }
    }
}
