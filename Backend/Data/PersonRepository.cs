using BachelorOppgave.Controllers.PersonsModels;
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
    public class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int CreatePerson(CreatePerson person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Execute(@"EXEC [dbo].[CreatePerson]
                    @name = @name,
                    @surname = @surname,
                    @username  = @username,
                    @email = @email,
                    @password = @password,
                    @postnr   = @postnr,
                    @address  = @address",
                    person
                 );
                return result;
            }
        }

        public IEnumerable<Person> FetchPersons()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Person>(@"EXEC[dbo].[FetchPerson]");
            }
        }

        //fetches individual user by using his/her 's  userID
        public Person FetchPersonByID(int userID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<Person>(@"EXEC[dbo].[FetchPersonByID] @personID = @personID");
            }
        }

        public int UpdatePerson(UpdatePerson updatePerson)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[UpdatePerson]
                    @personID = @personID
                    @name = @name,
                    @surname = @surname,
                    @username = @username,
                    @email = @email,
                    @password = @password,
                    @postnr = @postnr,
                    @address = @address",
                    updatePerson
                );
                return result;
            }
        }

        public int DeletePerson(int personID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[DeletePerson]
                    @personID = @personID",
                    new
                    {
                        personID,
                    }
                 );
                return result;
            }
        }
    }
}
