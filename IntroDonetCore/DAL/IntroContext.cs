﻿using IntroDonetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntroDonetCore.DAL
{


    // change from DbContext to IdentityDbContext
    public class IntroContext: IdentityDbContext
    {

        public DbSet<Course> Courses { get; set; }

         public  DbSet<Department> Departments { get; set; }

         public DbSet<Student> Students { get; set; }

         public  DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }




        public IntroContext(DbContextOptions options)
        :base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new EnrollmentConfig());
            modelBuilder.ApplyConfiguration(new InstructorConfig());
            modelBuilder.ApplyConfiguration(new CourseAssignmentConfig());
            modelBuilder.ApplyConfiguration(new OfficeAssignmentConfig());
        }


    }
}
