using BachelorOppgave.Controllers.LessonsModels;
using BachelorOppgave.Data.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BachelorOppgave.Data
{
    public class LessonRepository
    {

        private readonly string _connectionString;

        public LessonRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }


        public int CreateLesson(CreateLesson createLesson)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[CreateLesson]
                    @type = @type,
                    @moduleID = @moduleID, 
                    @name = @name, 
                    @details = @details",
                        createLesson
                    
                 );
                return result;
            }
        }

        public int UpdateLesson(Lesson lesson)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[UpdateLesson]
                    @lessonID = @lessonID,
                    @type = @type,
                    @moduleID = @moduleID, 
                    @name = @name, 
                    @details = @details",
                    lesson
                 );
                return result;
            }
        }

        public int DeleteLesson(int lessonID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[DeleteLesson]
                    @lessonID = @lessonID",
                    new
                    {
                        lessonID,
                    }
                 );
                return result;

            }
        }


        public IEnumerable<Lesson> FetchLessons()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Lesson>(@"EXEC [dbo].[FetchLesson]");
            }
        }

        public Lesson FetchLessonByID(int lessonID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<Lesson>(@"EXEC [dbo].[FetchLessonByID] @lessonID = @lessonID",
                    new
                    {
                        lessonID,
                    });
            }
        }
    }
}
