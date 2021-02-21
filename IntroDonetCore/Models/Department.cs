using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroDonetCore.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }


        public int? InstructorId { get; set; }

        [Display(Name = "Administrator")]
        public Instructor Instructor { get; set; }


        public ICollection<Course> Courses { get; set; }
    }
}
