using IntroDonetCore.Models;
using System;
using System.Collections.Generic;


namespace IntroDonetCore.ViewModels
{
    public class InstructorViewModel
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }

    }
}
