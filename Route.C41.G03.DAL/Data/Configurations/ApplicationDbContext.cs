using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Route.C41.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G03.DAL.Data.Configurations
{
public class ApplicationDbContext: IdentityDbContext
	{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

       //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		   //=> optionsBuilder.UseSqlServer("Server = .; Database = MVCApplicationG03; Trusted_Connection = True");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
			//modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());	
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}
		public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
