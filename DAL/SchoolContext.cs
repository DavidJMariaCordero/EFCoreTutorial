using EFCoreTutorial.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorial.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SchoolDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                    .Property(s => s.StudentId)
                    .HasColumnName("Id")
                    .HasDefaultValue(0)
                    .IsRequired();

            modelBuilder.Entity<Student>()
                    .HasOne<Grade>(s => s.Grade)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GradeId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                    .HasOne<StudentAddress>(s => s.Address)
                    .WithOne(ad => ad.Student)
                    .HasForeignKey<StudentAddress>(ad => ad.StudentAddressId);

            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                    .HasOne<Student>(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                    .HasOne<Course>(sc => sc.Course)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId);
        }
    }
}
