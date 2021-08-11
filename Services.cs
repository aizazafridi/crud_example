﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MyCRUD
{
    public class Services
    {
        private Repository repo = null;

        //Constructor initializes repository object
        public Services()
        {
            this.repo = new Repository();
        }

        //Method that opens an Sql Connection
        public void OpenConnection()
        {
            this.repo.OpenConnection();
        }

        //Method that closes the SqlDataReader connection
        public void CloseConnection()
        {
            this.repo.CloseConenction();
        }

        //Method that retrieves all records from Employee table
        public SqlDataReader GetEmployees()
        {
            SqlDataReader reader = this.repo.GetEmployees();
            return reader;
        }

        //Method that inserts a record into Employee table
        public void InsertEmployee(string first_name, string last_name, string username, string position, string salary)
        {
            this.repo.InsertEmployee(first_name, last_name, username, position, salary);
        }

        //Method that deletes a record from Employee table
        public void DeleteEmployee(string id)
        {
            this.repo.DeleteEmployee(id);
        }

        //Method that updates a record in Employee table
        public void UpdateEmployee(string first_name, string last_name, string username, string position, string salary, string id)
        {
            this.repo.UpdateEmployee(first_name, last_name, username, position, salary, id);
        }
    }
}
