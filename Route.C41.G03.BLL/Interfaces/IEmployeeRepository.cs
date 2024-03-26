﻿using Route.C41.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G03.BLL.Interfaces
{

   public interface IEmployeeRepository: IGenericRepository<Employee>
    {
    
        IQueryable<Employee> GetEmployeeByAddress(string address);  

        IQueryable<Employee> SearchByName(string name); 
    }
}
