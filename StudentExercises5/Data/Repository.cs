using System.Collections.Generic;
using System.Data.SqlClient;
using StudentExercises5.Models;

namespace StudentExercises5.Data
{
    public class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        // Exercises
        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        // SQL query text
                        cmd.CommandText = "SELECT Id, ExerciseName, ExerciseLanguage FROM Exercise";

                        // create reader to access data
                        SqlDataReader reader = cmd.ExecuteReader();

                        // retrieved exercises from database
                        List<Exercise> exercises = new List<Exercise>();

                        // Read will return true while there is more data to read
                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);

                            int exerciseNameColumnPosition = reader.GetOrdinal("ExerciseName");
                            string exerciseNameValue = reader.GetString(exerciseNameColumnPosition);

                            int exerciseLanguageColumnPosition = reader.GetOrdinal("ExerciseLanguage");
                            string exerciseLanguageValue = reader.GetString(exerciseLanguageColumnPosition);

                            // Create each Exercise Object
                            Exercise exercise = new Exercise
                            {
                                Id = idValue,
                                ExerciseName = exerciseNameValue,
                                ExerciseLanguage = exerciseLanguageValue
                            };

                            // Add each exercise object to exercises list
                            exercises.Add(exercise);
                        }

                        reader.Close();

                        // Return list of exercises
                        return exercises;
                    }
            }
        }
    }
}