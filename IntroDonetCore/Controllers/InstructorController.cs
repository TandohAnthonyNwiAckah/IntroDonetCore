using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;
using IntroDonetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IntroDonetCore.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseAssignmentRepository _courseAssignmentRepository;

        public InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, ICourseAssignmentRepository courseAssignmentRepository)
        {
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
            _courseAssignmentRepository = courseAssignmentRepository;
        }

        [Route("Instructor/Index/{id?}")]
        public async Task<IActionResult> Index(int? id, int? CourseId)
        {

            var allInstructors = await _instructorRepository.Instructors();

            var model = new InstructorViewModel(){

                Instructors = allInstructors
            
            };

            if (id != null)
            {
                var instructor = model.Instructors.FirstOrDefault(x => x.InstructorId == id);

                model.Courses = instructor.CourseAssignments.Select(s => s.Course);

                if (instructor != null) model.Courses = instructor.CourseAssignments.Select(s => s.Course);

            }

            if (CourseId != null)
            {

                ViewData["CourseId"] = CourseId.Value;

                model.Enrollments = model.Courses.FirstOrDefault(x => x.CourseId == CourseId).Enrollments;

            }

            return View(model);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructor = await _instructorRepository.Instructor((int)id);

            return View(constructor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var courses = _courseRepository.GetAll();

            var model = new EditInstructorViewModel()
               {
                   AssignedCourseData = courses.Select(s => new AssignedCourseData()
                   {
                       CourseId = s.CourseId,
                       CourseName = s.CourseName,
                       Assigned = false
                   }).ToList()
               };

               return View(model);

        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost(CreateInstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Add(model.Instructor);

                var instructorId = model.Instructor.InstructorId;

                var courseAssignment = new List<CourseAssignment>();

                if (model.AssignedCourseData != null)
                {
                    foreach (var data in model.AssignedCourseData)
                    {
                        if (data.Assigned)
                        {
                            //courseAssignment.Add(new CourseAssignment(){CourseId = data.CourseId,InstructorId = instructorId});
                            _courseAssignmentRepository.Add(new CourseAssignment()
                            { CourseId = data.CourseId, InstructorId = instructorId });
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            var allCourses = _courseRepository.GetAll();
            var coursesToInstructor = await _courseAssignmentRepository.CoursesToInstructorAsync(instructor.InstructorId);
            var model = new CreateInstructorViewModel()
            {
                Instructor = instructor,
                AssignedCourseData = allCourses.Select(s => new AssignedCourseData()
                {
                    CourseId = s.CourseId,
                    CourseName = s.CourseName,
                    Assigned = coursesToInstructor.Exists(x => x.Course.CourseId == s.CourseId)

                }).OrderBy(x => x.CourseName).ToList()
            };


            return View(model);

        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost(CreateInstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Update(model.Instructor);

                var instructorId = model.Instructor.InstructorId;

                if (model.AssignedCourseData != null)
                    foreach (var data in model.AssignedCourseData)
                {
                    {
                        if (data.Assigned)
                        {
                            var isExist = IsExist(_courseAssignmentRepository.GetAll(), instructorId, data.CourseId);

                            if (!isExist)
                            {
                                _courseAssignmentRepository.Add(new CourseAssignment()
                                { CourseId = data.CourseId, InstructorId = instructorId });
                            }

                        }
                        else
                        {

                            var isExist = IsExist(_courseAssignmentRepository.GetAll(), instructorId, data.CourseId);

                            if (isExist)
                            {

                                var filter = _courseAssignmentRepository
                                    .GetByFiler(x => x.CourseId == data.CourseId && x.InstructorId == instructorId)
                                    .FirstOrDefault();

                                _courseAssignmentRepository.Delete(filter);

                            }

                        }
                    }

                    return RedirectToAction("Index");
                }

                return View("Create");
            }

            return View("Edit");
        }

        private bool IsExist(IEnumerable<CourseAssignment> source, int instructorId, int courseId)
        {
            return source.Where(x => x.InstructorId == instructorId).Any(c => c.CourseId == courseId);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var instructor = await _instructorRepository.Instructor((int)id);
                if (instructor == null)
                {
                    return NotFound();
                }

                return View(instructor);
            }

            return View("Index");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? InstructorId)
        {
            if (InstructorId != null)
            {
                var instructor = _instructorRepository.GetById((int)InstructorId);
                if (instructor == null)
                {
                    return NotFound();
                }

                _instructorRepository.Delete(instructor);
                return RedirectToAction("Index");
            }

            return View("Index");
        }



    }
}