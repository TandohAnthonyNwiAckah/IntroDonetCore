using IntroDonetCore.Models;
using System.Collections.Generic;

namespace IntroDonetCore.Services.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {

        IEnumerable<Enrollment> CoursesToStudent(int studentId);

    }
}
