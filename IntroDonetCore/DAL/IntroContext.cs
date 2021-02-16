﻿using IntroDonetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroDonetCore.DAL
{
    public class IntroContext: DbContext
    {

        public DbSet<Course> Course { get; set; }

         public  DbSet<Department> Department { get; set; }

        public IntroContext(DbContextOptions options)
        :base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());

        }


    }
}
