using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models_Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public IConfiguration Configuration { get; set; }
        private string connectionString;
        public CustomerRepository(IConfiguration _configuration)
        {
            Configuration = _configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
        public Customers AddCustomer(Customers _customers)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoCustomer]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Name", _customers.Name);
                    cmd.Parameters.AddWithValue("@Address", _customers.Address);
                    cmd.Parameters.AddWithValue("@Telephone", _customers.Telephone);
                    cmd.Parameters.AddWithValue("@Email", _customers.Email);
                    cmd.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _customers;
        }

        public void DeleteCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteCustomer]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        public IEnumerable<Customers> GetAllCustomer()
        {
            List<Customers> _customers = new List<Customers>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectCustomer]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customers customers = new Customers();
                        customers.Id = Convert.ToInt32(reader["Id"]);
                        customers.Name = reader["Name"].ToString();
                        customers.Address = reader["Address"].ToString();
                        customers.Telephone = reader["Telephone"].ToString();
                        customers.Email = reader["Email"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return _customers;
        }

        public Customers GetCustomerById(int id)
        {
            Customers customers = new Customers();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectCustomerById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customers.Id = id;
                        customers.Name = reader["Name"].ToString();
                        customers.Address = reader["Address"].ToString();
                        customers.Telephone = reader["Telephone"].ToString();
                        customers.Email = reader["Email"].ToString();

                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return customers;
        }

        public Customers UpdateCustomer(Customers _customers)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateCustomer]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", _customers.Id);
                    cmd.Parameters.AddWithValue("@Name", _customers.Name);
                    cmd.Parameters.AddWithValue("@Address", _customers.Address);
                    cmd.Parameters.AddWithValue("@Telephone", _customers.Telephone);
                    cmd.Parameters.AddWithValue("@Email", _customers.Email);
                    cmd.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _customers;
        }
    }
}
