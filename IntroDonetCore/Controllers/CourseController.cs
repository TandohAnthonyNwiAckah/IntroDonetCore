using IntroDonetCore.DAL;
using IntroDonetCore.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace IntroDonetCore.Controllers
{
    public class CourseController : Controller
    {

        /* private readonly IntroContext _context;

                public CourseController(IntroContext context)
                {
                    _context = context;
                }*/

        private readonly ICourseRepository _courseRepository;

        public CourseController(IntroContext context, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public IActionResult Index()
        {
            /*

                        // Method Syntax
                        var methodSyntax = _context.Course.Include(x => x.Department).ToList();


                        // Query Syntax
                        var querySyntax = from dept in _context.Department
                                          join course in _context.Course on dept.DepartmentId equals course.DepartmentId
                                          select course;

                          return View(querySyntax);
            */


        /*    var allCourses = _courseRepository.GetAll();*/

            var allCourses = _courseRepository.CoursesToDepartment();

            return View(allCourses);


        }


    }
}
