using BachelorOppgave.Data.Models;
using BachelorOppgave.Controllers.AdminModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BachelorOppgave.Data
{
    public class AdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int CreateAdmin(CreateAdmin createAdmin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Execute(@"EXEC [dbo].[CreateAdmin]
                    @birthnumber = @birthnumber, 
                    @personID = @personID, 
                    @telNr = @telNr",
                    createAdmin
                 );
                return result;
            }
        }

        public IEnumerable<Admin> FetchAdmin()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Admin>(@"EXEC[dbo].[FetchAdmin]");
            }
        }


        public int UpdateAdmin(UpdateAdmin updateAdmin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[UpdateAdmin]
                    @adminID = @adminID,
                    @birthnumber = @birthnumber, 
                    @telNr = @telNr",
                   updateAdmin
                );
                return result;
            }
        }

        public int DeleteAdmin(int adminID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[DeleteAdmin]
                    @adminID = @adminID",
                    new
                    {
                        adminID,
                    }
                 );
                return result;
            }
        }


        //fetches individual admin by using his/her 's  adminID
        public Admin FetchAdminByID(int adminID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<Admin>(@"EXEC [dbo].[FetchAdminByID] @adminID = @adminID",
                    new
                    {
                        adminID,
                    });
            }
        }
    }
}
