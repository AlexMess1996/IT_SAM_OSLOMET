using BachelorOppgave.Controllers.TeacherModels;
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
    public class TeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int CreateTeacher(CreateTeacher createTeacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Execute(@"EXEC [dbo].[CreateTeacher]
                    @title = @title, 
                    @institution = @institution, 
                    @personID = @personID, 
                    @image = @image",
                    createTeacher
                 );
                return result;
            }
        }

        public IEnumerable<Teacher> FetchTeacher()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Teacher>(@"EXEC[dbo].[FetchTeacher]");
            }
        }

        //fetches individual teacher by using his/her 's  teacherID
        public Teacher FetchTeacherByID(int teacherID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<Teacher>(@"EXEC [dbo].[FetchTeacherByID] @teacherID = @teacherID",
                    new
                    {
                        teacherID,
                    });
            }
        }
        public int UpdateTeacher(UpdateTeacher updateTeacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[UpdateTeacher]
                    @teacherID = @teacherID, 
                    @title = @title, 
                    @institution = @institution, 
                    @image = @image",
                    updateTeacher
                );
                return result;
            }
        }

        public int DeleteTeacher(int teacherID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[DeleteTeacher]
                    @teacherID = @teacherID",
                    new
                    {
                        teacherID,
                    }
                 );
                return result;
            }
        }

    }
}
