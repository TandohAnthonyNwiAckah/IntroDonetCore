using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroDonetCore.DAL;
using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace IntroDonetCore.Services.Repository
{
    public class CourseAssignmentRepository : Repository<CourseAssignment>, ICourseAssignmentRepository
    {
        private readonly IntroContext _context;


        public CourseAssignmentRepository(IntroContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<CourseAssignment>> CoursesToInstructorAsync(int id)
        {
            return await _context.CourseAssignments
                .Where(x => x.CourseId == id)
                .Include(x => x.Course)
                .ToListAsync();
        }

        public List<CourseAssignment> CoursesToInstructor(int id)
        {
            return _context.CourseAssignments
                .Where(x => x.CourseId == id)
                .Include(x => x.Course)
                .ToList();
        }
    }

}
