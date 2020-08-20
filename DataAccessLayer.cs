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

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("procName", connection) { 
                    CommandType = CommandType.StoredProcedure }) 
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("orderDate", orderDate));

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new Person {
                        PersonId = int.Parse(reader["personId"].ToString()),
                        NameFirst = reader["nameFirst"].ToString(),
                        NameLast = reader["nameLast"].ToString()
                    });
                }
            }

            return results;
        }
    }
}

/*
using (var conn = new SqlConnection(connectionString))
using (var command = new SqlCommand("ProcedureName", conn) { 
                           CommandType = CommandType.StoredProcedure }) {
   conn.Open();
   command.ExecuteNonQuery();
}
*/