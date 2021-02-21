using IntroDonetCore.Models;
using System.Collections.Generic;


namespace IntroDonetCore.ViewModels
{
    public class CreateInstructorViewModel
    {

        public Instructor Instructor { get; set; }
        public List<AssignedCourseData> AssignedCourseData { get; set; }

    }
}
