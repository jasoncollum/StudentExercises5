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

        }
        // Functionality
        public static void PrintExerciseReport(string title, List<Exercise> exercises)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            exercises.ForEach(ex => Console.WriteLine($"{ex.Id}. {ex.ExerciseName}"));
            Console.WriteLine();
        }
    }
}
