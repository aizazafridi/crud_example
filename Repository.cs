﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MyCRUD
{
    public class Repository
    {
        private SqlConnection conn = null;
        private string conn_string = "Data Source=AIZAZ-PC;Initial Catalog=NewDB;Integrated Security=True";

        //Constructor conencts to the databae and sets conn property
        public Repository()
        {
            //Connect to SQL Database
            try
            {
                this.conn = new SqlConnection(conn_string);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }         
        }

        //Method that opens an Sql Connection
        public void OpenConnection()
        {
            this.conn.Open();
        }

        //method that closes the open connection
        public void CloseConenction()
        {
            this.conn.Close();
        }

        //Method that retrieves all records from Employee table and returns SqlDataReader object
        public SqlDataReader GetEmployees()
        {
            //Query to retrieve all records from the Employee Table
            string query = "SELECT * FROM Employee";
            //Creating a new instance of SqlCommand
            SqlCommand cmd = new SqlCommand(query, this.conn);
            //Executing the query, returns an SqlDataReader object
            SqlDataReader sdr = cmd.ExecuteReader();
            //Return SqlDataReader object
            return sdr;
        }

        //Method that inserts data into Employee table
        public void InsertEmployee(string first_name, string last_name, string username, string position, string salary)
        {
            //Query to insert data into Employee table
            string query = "INSERT INTO Employee VALUES (@FirstName,@LastName,@UserName,@Position,@Salary)";
            try
            {
                SqlCommand cmd = new SqlCommand(query, this.conn);
                //Setting command type
                cmd.CommandType = CommandType.Text;
                //Passing parameters
                cmd.Parameters.AddWithValue("@FirstName", first_name);
                cmd.Parameters.AddWithValue("@LastName", last_name);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Salary", salary);
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Method that deletes a record in Employee table
        public void DeleteEmployee(string id)
        {
            string query = "DELETE FROM Employee WHERE ID = @ID";
            try
            {
                SqlCommand cmd = new SqlCommand(query, this.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Method that updates a record in Employee table
        public void UpdateEmployee(string first_name, string last_name, string username, string position, string salary, string id)
        {
            string query = "UPDATE EMPLOYEE SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Position = @Position, Salary = @Salary WHERE ID = @ID";
            try
            {
                SqlCommand cmd = new SqlCommand(query, this.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FirstName", first_name);
                cmd.Parameters.AddWithValue("@LastName", last_name);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Salary", salary);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
