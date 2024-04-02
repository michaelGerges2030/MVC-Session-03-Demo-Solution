using Route.C41.G03.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;
using Microsoft.AspNetCore.Http;

namespace MVC_Session_03_Demo.ViewModels
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }

    public enum EmpType
    {
        [EnumMember(Value = "FullTime")]
        FullTime = 1,
        [EnumMember(Value = "PartTime")]
        PartTime = 2
    }
    public class EmployeeViewModel
    {
           public int Id { get; set; }

            [MinLength(5, ErrorMessage = "Min Length Of Name Is 5 Chars")]
            public string Name { get; set; }

            [Range(22, 30)]
            public int? Age { get; set; }

            [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
                ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
            public string Address { get; set; }

            [DataType(DataType.Currency)]
            public decimal Salary { get; set; }

            [Display(Name = "Is Active")]
            public bool IsActive { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            public Gender Gender { get; set; }

            [Display(Name = "Employee Type")]
            public EmpType EmployeeType { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }

            [Display(Name = "Hiring Date")]
            public DateTime HiringDate { get; set; }

        //[Display(Name = "Is Deleted?")]
        //public bool IsDeleted { get; set; } = false;
        //[Display(Name = "Creation Date")]
        //public DateTime CreationDate { get; set; } = DateTime.Now;

            public int? DepartmentId { get; set; }
            public Department Department { get; set; }
            public IFormFile Image { get; set; }
            public string ImageName { get; set; }

    }
    }
