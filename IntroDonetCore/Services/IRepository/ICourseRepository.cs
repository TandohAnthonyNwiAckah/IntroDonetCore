using IntroDonetCore.Models;
using System.Collections.Generic;

namespace IntroDonetCore.Services.IRepository
{
  public interface ICourseRepository : IRepository<Course>
    {
    IEnumerable<Course> CoursesToDepartment();

    }
}
