using BachelorOppgave.Controllers.ModulesModels;
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
    public class ModuleRepository
    {

        private readonly string _connectionString;

        public ModuleRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int CreateModule(CreateModule createModule)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"exec [dbo].[CreateModule]
                    @title = @title, 
                    @teacherID = @teacherID,
                    @adminID = @adminID, 
                    @institution = @institution,
                    @price = @price,
                    @description = @description, 
                    @language = @language, 
                    @picture = @picture,
                    @subject = @subject,
                    @duration = @duration",
                    createModule
                 );
                return result;
            }
        }

        public int UpdateModule(UpdateModule updateModule)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"exec [dbo].[UpdateModule]
                    @moduleID = @moduleID,
                    @title = @title, 
                    @teacherID = @teacherID,
                    @adminID = @adminID, 
                    @institution = @institution,
                    @price = @price,
                    @description = @description, 
                    @language = @language, 
                    @picture = @picture,
                    @subject = @subject,
                    @duration = @duration",
                    updateModule
                 );
                return result;
            }
        }

            public IEnumerable<Module> FetchModules()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<Module>(@"EXEC [dbo].[FetchModule]");
            }
        }

        public Module FetchModuleByID(int moduleID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Dapper QueryFirstOrDefault method is used to fetch one record.
                return connection.QueryFirstOrDefault<Module>(@"EXEC [dbo].[FetchModuleByID] @moduleID = @moduleID",
                    new
                    {
                        moduleID,
                    });
            }
        }

        public int DeleteModule(int moduleID)
        {
           
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = connection.Execute(@"EXEC [dbo].[DeleteModule]
                    @moduleID = @moduleID",
                    new
                    {
                        moduleID,
                    }
                 );
                return result;

            }
        }

    }
}
