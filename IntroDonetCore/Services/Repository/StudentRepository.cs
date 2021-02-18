using IntroDonetCore.DAL;
using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntroDonetCore.Services.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {

        private readonly IntroContext _context;
        public StudentRepository(IntroContext context) : base(context)
        {
            this._context = context;
        }

        public IEnumerable<Enrollment> CoursesToStudent(int studentId)
        {
            return _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(x => x.Student)
                .Include(x => x.Course)
                .ToList();
        }
    }

}

