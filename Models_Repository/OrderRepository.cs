using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models_Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IConfiguration Configuration { get; set; }
        private string connectionString;
        public OrderRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
        public Order AddOrder(Order _order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //_logger.LogInformation("Could break here!!");
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoOrder]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@CustomerId", _order.CustomerId);
                    cmd.Parameters.AddWithValue("@Description", _order.Description);
                    cmd.Parameters.AddWithValue("@OrderCost", _order.OrderCost);
                    // cmd.Parameters.AddWithValue("@ret", ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _order;
        }

        public void DeleteOrder(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteOrder]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();

                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        public IEnumerable<Order> GetAllOrder()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectOrder]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Order _order = new Order();
                        _order.Id = Convert.ToInt32(rdr["Id"]);
                        _order.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
                        _order.Description = rdr["Description"].ToString();
                        _order.OrderCost = Convert.ToDecimal(rdr["OrderCost"]);
                        orders.Add(_order);
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return orders;
        }
        public Order GetOrderById(int id)
        {
            Order _order = new Order();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectOrderById]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _order.Id = Convert.ToInt32(reader["Id"]);
                        _order.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        _order.Description = reader["Description"].ToString();
                        _order.OrderCost = Convert.ToDecimal(reader["OrderCost"]);
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _order;
        }

        public Order UpdateOrder(Order _order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateOrder]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", _order.Id);
                    cmd.Parameters.AddWithValue("@CustomerId", _order.CustomerId);
                    cmd.Parameters.AddWithValue("@Description", _order.Description);
                    cmd.Parameters.AddWithValue("@OrderCost", _order.OrderCost);
                    cmd.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _order;
        }
    }
}
