using IntroDonetCore.DAL;
using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntroDonetCore.Services.Repository
{

    public class CourseRepository : Repository<Course>, ICourseRepository
    {

        private readonly IntroContext _context;

        public CourseRepository(IntroContext _context, IntroContext context) : base(_context)
        {
            this._context = context;
        }

        public IEnumerable<Course> CoursesToDepartment()
        {
            return _context.Course.Include(x => x.Department).ToList();
        }

    }

}