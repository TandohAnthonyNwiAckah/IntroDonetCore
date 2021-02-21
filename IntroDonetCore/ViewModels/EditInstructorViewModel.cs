using IntroDonetCore.Models;
using System;
using System.Collections.Generic;


namespace IntroDonetCore.ViewModels
{
    public class EditInstructorViewModel
    {
        public Instructor Instructor { get; set; } 
        public List<AssignedCourseData> AssignedCourseData { get; set; }
    }
}
