using StudyGuide.Entities;
using StudyGuide.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace StudyGuide.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString;

        public OrderRepository()
        {
            _connectionString = DbConnectionStringProvider.Instance.ConnectionString;
        }
        public void AddOrder(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_AddOrder", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.Parameters.AddWithValue("@StudyGuideId", order.StudyGuideId);
                command.Parameters.AddWithValue("@IsCompleted", order.IsCompleted);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void FullFillOrder(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_FullFillOrder", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@IsCompleted", order.IsCompleted);
                command.Parameters.AddWithValue("@OrderCompletedDate", order.OrderCompletedDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            var Orders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_GetOrders", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(reader["OrderCompletedDate"].ToString()))
                    {
                        Orders.Add(new Order
                    {
                        OrderId = Convert.ToInt32(reader["OrderId"].ToString()),
                        CustomerId = reader["CustomerId"].ToString(),
                        StudyGuideId = Convert.ToInt32(reader["StudyGuideId"].ToString()),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"].ToString()),
                        OrderCompletedDate = null
                    });
                    }
                    else
                    {
                        Orders.Add(new Order
                    {
                        OrderId = Convert.ToInt32(reader["OrderId"].ToString()),
                        CustomerId = reader["CustomerId"].ToString(),
                        StudyGuideId = Convert.ToInt32(reader["StudyGuideId"].ToString()),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"].ToString()),
                        OrderCompletedDate = Convert.ToDateTime(reader["OrderCompletedDate"].ToString())
                    });
                    }
                }
            }

            return Orders;
        }
    }
}
