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
        // Get All Exercises
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

        // Get All Exercises By Language
        public List<Exercise> GetExercisesByLanguage(string language)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // SQL query text
                    cmd.CommandText = $@"SELECT Id, ExerciseName, ExerciseLanguage 
                                        FROM Exercise
                                        WHERE ExerciseLanguage = '{language}'";

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

        // Add A New Exercise
        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Exercise (ExerciseName, ExerciseLanguage) 
                                        VALUES ('{exercise.ExerciseName}', '{exercise.ExerciseLanguage}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Instructor> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // SQL query text
                    cmd.CommandText = @"SELECT i.Id, i.FirstName, i.LastName, i.SlackHandle, i.CohortId, i.Specialty, c.CohortName 
                                        FROM Instructor i
                                        JOIN Cohort c ON i.CohortId = c.Id";

                    // create reader to access data
                    SqlDataReader reader = cmd.ExecuteReader();

                    // retrieved instructors from database with cohort name
                    List<Instructor> instructors = new List<Instructor>();

                    // Read will return true while there is more data to read
                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        int cohortIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortIdValue = reader.GetInt32(cohortIdColumnPosition);

                        int cohortNameColumnPosition = reader.GetOrdinal("CohortName");
                        string cohortNameValue = reader.GetString(cohortNameColumnPosition);

                        // Create each Instructor Object
                        Instructor instructor = new Instructor
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            CohortId = cohortIdValue,
                            CohortName = cohortNameValue
                        };

                        // Add each instructor object to instructors list
                        instructors.Add(instructor);
                    }

                    reader.Close();

                    // Return list of instructors
                    return instructors;
                }
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Instructor (FirstName, LastName, SlackHandle, CohortId, Specialty) 
                                        VALUES ('{instructor.FirstName}', '{instructor.LastName}', '{instructor.SlackHandle}', '{instructor.CohortId}', '{instructor.Specialty}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddAssignment(int instructorId, int studentId, int exerciseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Assignment (InstructorId, StudentId, ExerciseId) 
                                        VALUES ('{instructorId}', '{studentId}', '{exerciseId}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // SQL query text
                    cmd.CommandText = @"SELECT s.Id, s.FirstName, s.LastName, s.SlackHandle, s.CohortId, c.CohortName
                                        FROM Student s
                                        JOIN Cohort c ON s.CohortId = c.Id";

                    // create reader to access data
                    SqlDataReader reader = cmd.ExecuteReader();

                    // retrieved students from database
                    List<Student> students = new List<Student>();

                    // Read will return true while there is more data to read
                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        int cohortIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortIdValue = reader.GetInt32(cohortIdColumnPosition);

                        int cohortNameColumnPosition = reader.GetOrdinal("CohortName");
                        string cohortNameValue = reader.GetString(cohortNameColumnPosition);

                        // Create each Student Object
                        Student student = new Student
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            CohortId = cohortIdValue,
                            CohortName = cohortNameValue
                        };

                        // Add each exercise object to exercises list
                        students.Add(student);
                    }

                    reader.Close();

                    // Return list of exercises
                    return students;
                }
            }
        }
    }
}