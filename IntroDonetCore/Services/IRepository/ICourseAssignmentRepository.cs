using IntroDonetCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IntroDonetCore.Services.IRepository
{
    public interface ICourseAssignmentRepository:IRepository<CourseAssignment>
    {
        Task<List<CourseAssignment>> CoursesToInstructorAsync(int id);
        List<CourseAssignment> CoursesToInstructor(int id);
    }
}