using StudyGuide.DataAccess.Interfaces;
using StudyGuide.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace StudyGuide.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string _connectionString;

        public CustomerRepository() 
        {
            _connectionString = DbConnectionStringProvider.Instance.ConnectionString;
        }
        public void AddCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_AddCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@CustomerEmail", customer.CustomerEmail);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var Customers = new List<Customer>();

            using(var connection = new SqlConnection(_connectionString)) 
            {
                var command = new SqlCommand("SP_GetCustomers", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customers.Add(new Customer
                    {
                        CustomerId = reader["CustomerId"].ToString(),
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerEmail = reader["CustomerEmail"].ToString()
                    });
                }
            }

            return Customers;
        }
    }
}
