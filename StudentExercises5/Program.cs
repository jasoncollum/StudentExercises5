using System;
using System.Collections.Generic;
using System.Linq;
using StudentExercises5.Data;
using StudentExercises5.Models;

namespace StudentExercises5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Repository
            Repository repository = new Repository();

            List<Exercise> exercises = repository.GetAllExercises();

            PrintExerciseReport("All exercises: ", exercises);
            Pause();

            // Get All JavaScript Exercises
            List<Exercise> jsExercises = repository.GetExercisesByLanguage("JavaScript");

            PrintExerciseReport("All JS exercises: ", jsExercises);
            Pause();

            List<Student> students = repository.GetAllStudents();

            // Add A New Exercise
            //Exercise repoCopyPasta = new Exercise
            //{
            //    ExerciseName = "Repository Copy Pasta",
            //    ExerciseLanguage = "C Sharp"
            //};

            //repository.AddExercise(repoCopyPasta);

            //exercises = repository.GetAllExercises();
            //PrintExerciseReport("All exercises: ", exercises);
            //Pause();

            // Find Instructors and their Cohorts
            List<Instructor> instructors = repository.GetAllInstructors();

            PrintInstructorReport("All instructors: ", instructors);
            Pause();

            // Add A New Instructor
            //Instructor kristenNorris = new Instructor
            //{
            //    FirstName = "Kristen",
            //    LastName = "Norris",
            //    SlackHandle = "Kristen Norris",
            //    CohortId = 2,
            //    Specialty = "Problem Solving"
            //};

            //repository.AddInstructor(kristenNorris);

            //instructors = repository.GetAllInstructors();
            //PrintInstructorReport("All instructors: ", instructors);
            //Pause();

            // Assign an existing exercise to an existing student
            var adamScheaffer = instructors.First(i => $"{i.FirstName} {i.LastName}" == "Adam Scheaffer");
            var colinSandlin = students.First(s => $"{s.FirstName} {s.LastName}" == "Colin Sandlin");
            var asyncAwait = exercises.First(e => $"{e.ExerciseName}" == "Async Await");
            repository.AddAssignment(adamScheaffer.Id, colinSandlin.Id, asyncAwait.Id);

        }
        // Functionality
        public static void PrintExerciseReport(string title, List<Exercise> exercises)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            exercises.ForEach(ex => Console.WriteLine($"{ex.Id}. {ex.ExerciseName}"));
            Console.WriteLine();
        }

        public static void PrintInstructorReport(string title, List<Instructor> instructors)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            instructors.ForEach(i => Console.WriteLine($"{i.Id}. {i.FirstName} {i.LastName}, {i.CohortName}"));
            Console.WriteLine();
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
