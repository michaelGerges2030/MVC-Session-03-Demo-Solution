using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G03.DAL.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum EmpType
    {
        FullTime = 1,
        PartTime = 2
    }
    public class Employee: ModelBase
    {
        public string Name { get; set; }

        public int? Age { get; set; }
        public string Address { get; set; }

        public decimal Salary { get; set; }
        public bool IsActive{ get; set; }
        public string Email { get; set; }

        public Gender Gender { get; set; }
       
        public EmpType EmployeeType { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }

        //public bool IsDeleted { get; set; } = false;
        //public DateTime CreationDate { get; set; } = DateTime.Now;

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
