using IntroDonetCore.Models;
using System.Collections.Generic;


namespace IntroDonetCore.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
