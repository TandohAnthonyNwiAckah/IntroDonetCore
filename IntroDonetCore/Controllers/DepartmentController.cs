
using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace IntroDonetCore.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstructorRepository _instructorRepository;

        public DepartmentController(IDepartmentRepository departmentRepository, IInstructorRepository instructorRepository)
        {
            _departmentRepository = departmentRepository;
            _instructorRepository = instructorRepository;
        }

        public IActionResult Index()
        {
            var departments = _departmentRepository.InstructorToDepartments();


            /*            var departments = _departmentRepository.GetAll();*/


            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = _departmentRepository.InstructorToDepartment(id);


/*            var department = _departmentRepository.GetById(id);*/


            if (department ==null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
           InstructorList();
         
            return View();
         }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Department model)
               {
                   if (ModelState.IsValid)
                   {
                       _departmentRepository.Add(model);
                   }

                   return RedirectToAction("Details", new {Id = model.DepartmentId});
               }

        public IActionResult Edit(int id)
        {

            /*   var department = _departmentRepository.InstructorToDepartment(id)*/


            var department = _departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            InstructorList();

            return View(department);

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Department model)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(model);
                 
                return RedirectToAction("Details", new { id = model.DepartmentId });
            }

            return View("Edit");
        }


        public void InstructorList()
        {
            ViewBag.Instructors = _instructorRepository.GetAll();
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _departmentRepository.InstructorToDepartment(id);

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int DepartmentId)
        {
            var department = _departmentRepository.GetById(DepartmentId);
            if (department == null)
            {
                return NotFound();
            }

            _departmentRepository.Delete(department);
            return RedirectToAction("Index");
        }



    }
}