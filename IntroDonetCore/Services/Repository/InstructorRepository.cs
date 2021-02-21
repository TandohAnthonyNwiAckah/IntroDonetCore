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
    public class InstructorRepository:Repository<Instructor>,IInstructorRepository
    {


        private readonly IntroContext _context;


        public InstructorRepository(IntroContext context) : base(context)
        {

            this._context = context;

        }

        public async  Task<IEnumerable<Instructor>> Instructors()
        {
            return await _context.Instructors
                .Include(x => x.OfficeAssignment)
                .Include(x => x.CourseAssignments)
                    .ThenInclude(x => x.Course)
                        .ThenInclude(x => x.Department)
                .Include(x => x.CourseAssignments)
                    .ThenInclude(x => x.Course)
                        .ThenInclude(x => x.Enrollments)
                            .ThenInclude(x => x.Student)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<Instructor> Instructor(int id)
        {
            return await _context.Instructors
                .Include(x => x.CourseAssignments)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(i => i.InstructorId == id);


        }


    }
}
