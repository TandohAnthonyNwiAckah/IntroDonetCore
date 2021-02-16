using System.Collections.Generic;
using System.Linq;
using IntroDonetCore.DAL;
using IntroDonetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroDonetCore.Services.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {

       private readonly IntroContext _context;

        public DepartmentRepository(IntroContext context) : base(context)
        {

            this._context = context;

        }

    /*    public IEnumerable<Department> InstructorToDepartments()
        {
            return _context.Department.Include(x => x.Instructor).ToList();

        }

        public Department InstructorToDepartment(int id)
        {
            //return  (from department in LeLeContext.Departments
            //    join instructor in LeLeContext.Instructors on department.Id equals instructor.Id
            //    select department).FirstOrDefault(x=>x.DepartmentId ==id);

            return _context.Department.Include(x => x.Instructor).FirstOrDefault(x => x.Id == id);
        }
*/
    }
}
