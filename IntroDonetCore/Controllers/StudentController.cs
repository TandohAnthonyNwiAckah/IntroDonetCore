using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using IntroDonetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System.Linq;

namespace IntroDonetCore.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

   
        public IActionResult Index(string sortOrder, string searchString, int pageindex = 1)
        {
            //if (string.IsNullOrEmpty(sortOrder))
            //{
            //    ViewData["sortName"] = "name_desc";
            //}
            //else
            //{
            //    ViewData["sortName"] = "";
            //}
            ViewData["sortName"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewData["sortByDate"] = sortOrder == "Date" ? "date_desc" : "Date";

            ViewData["currentFilter"] = searchString;

            //if (sortOrder=="Date")
            //{
            //    ViewData["sortByDate"] = "date_desc";
            //}
            //else
            //{
            //    ViewData["sortByDate"] = "Date";
            //}


            var students = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                               s.LastName.ToLower().Contains(searchString.ToLower()));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;

            }

            var model = PagingList.Create(students, 2, pageindex);

            return View(model);

       
        }



        [HttpGet]
        public IActionResult Create() {

         return View();
        }

        [HttpPost, ActionName("CreateStudent")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(Student model)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Add(model);
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        public IActionResult Edit(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null && id == 0)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("EditStudent")]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(Student model)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Update(model);
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        public IActionResult Delete(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null && id == 0)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStudent(int StudentId)
        {
            var course = _studentRepository.GetById(StudentId);
            if (course == null && StudentId == 0)
            {

                return NotFound();
            }

            _studentRepository.Delete(course);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

/*            ViewBag.Courses = _courseRepository.GetAll();*/

            var student = _studentRepository.GetById(id);

            var model = new StudentViewModel()
            {
                Student = student,
                Enrollments = _studentRepository.CoursesToStudent(student.StudentId)
            };

            return View(model);

        }


    }
}
