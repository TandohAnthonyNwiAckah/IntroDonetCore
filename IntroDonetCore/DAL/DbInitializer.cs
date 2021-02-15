﻿using IntroDonetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace IntroDonetCore.DAL
{
    public static class DbInitializer
    {

        public static void Seed(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IntroContext>();

                if (!context.Department.Any())
                {
                    context.Department.AddRange(Departments.Values);
                }

                if (!context.Course.Any())
                {
                    var courseList = new List<Course>
                    {
                        new Course() {CourseName = "C#", Department = Departments["Programming"], Credits = 8},
                        new Course() {CourseName = "CCNA", Department = Departments["Network"], Credits = 8},
                        new Course() {CourseName = "HTML", Department = Departments["Design"], Credits = 8}
                    };

                    context.Course.AddRange(courseList);
                }

                context.SaveChanges();
            }
        }

        private static Dictionary<string, Department> _departments;

        public static Dictionary<string, Department> Departments
        {

            get
            {
                if (_departments != null)
                {
                    return _departments;
                }

                var deptList = new[]
                {
                    new Department() {DepartmentName = "Programming"},
                    new Department() {DepartmentName = "Design"},
                    new Department() {DepartmentName = "Network"}
                };

                _departments = new Dictionary<string, Department>();

                foreach (var department in deptList)
                {
                    _departments.Add(department.DepartmentName, department);
                }

                return _departments;
            }
        }


    }
}
