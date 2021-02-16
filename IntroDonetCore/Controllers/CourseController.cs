using IntroDonetCore.DAL;
using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IntroDonetCore.Controllers
{
    public class CourseController : Controller
    {

        private readonly IntroContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CourseController(IntroContext context, ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            _courseRepository = courseRepository;
            _context = context;
            _departmentRepository = departmentRepository;
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

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Department = _departmentRepository.GetAll();

            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Course model)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Add(model);
                return RedirectToAction("Index");
            }

            return View("Create");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseRepository.GetById(id);

            if (course == null) {

                return NotFound();

            }

            ViewBag.Department = _departmentRepository.GetAll();

            return View(course);

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Course model)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Update(model);
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null && id == 0)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int CourseId) {
            var course = _courseRepository.GetById(CourseId);
            if (course == null && CourseId == 0)
            {
                return NotFound();
            }
            _courseRepository.Delete(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id) {

            /*  var course = _courseRepository.GetById(id);*/


            var course = _courseRepository.CoursesToDepartment().FirstOrDefault(x => x.CourseId == id);

            if (course == null && id == 0)
            {
                return NotFound();
            }

            return View(course);

        }



    }
}
