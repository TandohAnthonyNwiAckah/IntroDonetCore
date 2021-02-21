using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntroDonetCore.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string LastName { get; set; }
       
        public string FirstName { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }



        public string FullName => $"{LastName} {FirstName}"; 


        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }

    }
}
