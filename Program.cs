using EFCoreTutorial.DAL;
using EFCoreTutorial.Entidades;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                var std = new Student()
                {
                    FirstName = "Bill",
                    LastName = "Gates"
                };
                context.Students.Add(std);

                context.SaveChanges();
            }

            using (var context = new SchoolContext())
            {
                var std = context.Students.First<Student>();
                std.FirstName = "Steve";
                context.SaveChanges();
            }

            using (var context = new SchoolContext())
            {
                var std = context.Students.First<Student>();
                context.Students.Remove(std);

                context.SaveChanges();
            }

            

            using (var context = new SchoolContext())
            {
                var stud = new Student() { StudentId = 1, FirstName = "Bill" };
                stud.FirstName = "Steve";

                context.Update<Student>(stud);
                context.SaveChanges();
            }

            using (var context = new SchoolContext())
            {
                var student = new Student()
                {
                    StudentId = 50
                };

                context.Remove<Student>(student);

                context.SaveChanges();
            }

            using (var context = new SchoolContext())
            {
                var student = context.Students.First();
                DisplayStates(context.ChangeTracker.Entries());
            }

            using (var context = new SchoolContext())
            {
                var students = context.Students
                   .FromSqlRaw("Select * from Students where Name = 'Bill'")
                   .ToList();
            }
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: { entry.State.ToString()}");
            }
        }
    }
}
