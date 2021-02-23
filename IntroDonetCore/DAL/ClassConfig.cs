using IntroDonetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntroDonetCore.DAL
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {

        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(k => k.CourseId);
            builder.Property(p => p.CourseId).ValueGeneratedOnAdd();
            builder.Property(p => p.Credits).IsRequired();

            builder.HasOne(d => d.Department)
               .WithMany(c => c.Courses)
                .HasForeignKey(f => f.DepartmentId);





        }
    }

    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(k => k.DepartmentId);
            builder.Property(p => p.DepartmentId).ValueGeneratedOnAdd();
            builder.Property(p => p.DepartmentName).IsRequired().HasColumnType("Nvarchar(50)");
            builder.Property(p => p.Budget).IsRequired();


            //builder.HasMany(m => m.Courses)
            //    .WithOne(o => o.Department)
            //    .HasForeignKey(f => f.DepartmentId);



            builder.HasOne(i => i.Instructor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);


        }
    }

    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(50)");
            builder.Property(p => p.LastName).HasColumnType("nvarchar(50)");
            builder.Property(p => p.EnrollmentDate).HasColumnType("Date").HasDefaultValueSql("GetDate()");
        }
    }

    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.ErollmentId);
            builder.Property(p => p.Grade).IsRequired();


            builder.HasOne(s => s.Student)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(s => s.StudentId);

            builder.HasOne(s => s.Course)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(c => c.CourseId);

        }
    }

    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(k => k.InstructorId);
            builder.Property(p => p.LastName).HasMaxLength(25);
            builder.Property(p => p.LastName).HasMaxLength(25);
            builder.Property(p => p.HireDate).HasColumnType("Date").HasDefaultValueSql("GetDate()");
            builder.Ignore(p => p.FullName);

            builder.HasOne(o => o.OfficeAssignment)
                .WithOne(i => i.Instructor)
                .HasForeignKey<OfficeAssignment>(i => i.InstructorId);
        }


    }

    public class CourseAssignmentConfig : IEntityTypeConfiguration<CourseAssignment>
    {
        public void Configure(EntityTypeBuilder<CourseAssignment> builder)
        {
            builder.HasKey(k => new { k.CourseId, InstructorId = k.InstructorId });

            builder.HasOne(i => i.Instructor)
                .WithMany(ca => ca.CourseAssignments)
                .HasForeignKey(i => i.InstructorId);

            builder.HasOne(c => c.Course)
                .WithMany(ca => ca.CourseAssignments)
                .HasForeignKey(c => c.CourseId);
        }
    }

    public class OfficeAssignmentConfig : IEntityTypeConfiguration<OfficeAssignment>
    {
        public void Configure(EntityTypeBuilder<OfficeAssignment> builder)
        {
            builder.HasKey(k => k.InstructorId);
        }
    }


}
