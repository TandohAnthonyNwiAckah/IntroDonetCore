using IntroDonetCore.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroDonetCore.Controllers
{
    public class CourseController : Controller
    {


        private readonly IntroContext _context;

        public CourseController(IntroContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            // Method Syntax
            var methodSyntax = _context.Course.Include(x => x.Department).ToList();


            // Query Syntax
            var querySyntax = from dept in _context.Department
                              join course in _context.Course on dept.DepartmentId equals course.DepartmentId
                              select course;


            return View(querySyntax);

           
        }
    }
}
