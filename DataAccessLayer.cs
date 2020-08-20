using System;
using System.Collections.Generic;
using System.Data;
using KeonaDataLayer.Models;
using Microsoft.Data.SqlClient;

namespace KeonaDataLayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly string _connectionString;

        public DataAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Person> GetPeopleWithOrders(DateTime orderDate)
        {
            var results = new List<Person>();

            try 
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("dbo.p_get_people_with_orders", connection) { 
                        CommandType = CommandType.StoredProcedure }) 
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("OrderDate", orderDate));

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Person {
                            PersonId = int.Parse(reader["PersonId"].ToString()),
                            NameFirst = reader["NameFirst"].ToString(),
                            NameLast = reader["NameLast"].ToString()
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                // TODO: log exception here, this will help us fix or troubleshoot issues 
                throw new Exception("Unable to retrieve data.");    
            }

            return results;
        }
    }
}
