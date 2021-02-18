using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntroDonetCore.Models
{
    public class Course
    {
  
      //  [Key()]
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CourseId { get; set; }

        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "Please fill the course Title")]
        public string CourseName { get; set; }

        public int Credits { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }


}
