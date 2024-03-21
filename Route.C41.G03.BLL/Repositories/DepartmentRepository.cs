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
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _dbCcontext;
		
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbCcontext = dbContext;
        }

        public int Add(Department entity)
		{
			_dbCcontext.Departments.Add(entity);
			return _dbCcontext.SaveChanges();
		}

		public int Update(Department entity)
		{
			_dbCcontext.Departments.Update(entity);
			return _dbCcontext.SaveChanges();
		}

		public int Delete(Department entity)
		{
		    _dbCcontext.Departments.Remove(entity);
			return _dbCcontext.SaveChanges();
		}

		public Department Get(int id)
		{
			//var department = _dbCcontext.Departments.Local.Where( D => D.Id == id).FirstOrDefault();
			//if (department == null)
			//	department = _dbCcontext.Departments.Where(D => D.Id == id).FirstOrDefault();
			//return department;

			//return _dbCcontext.Find<Department>(id);
			return _dbCcontext.Departments.Find(id);

		}

		public IEnumerable<Department> GetAll()
		{
			return _dbCcontext.Departments.AsNoTracking().ToList();
		}
	}
}
