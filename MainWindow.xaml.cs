using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace MyCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Initializing all the UI components
            InitializeComponent();
            //Load the employee table data to the data grid
            LoadGrid();
        }

        //Connect to SQL Database
        SqlConnection conn = new SqlConnection("Data Source=AIZAZ-PC;Initial Catalog=NewDB;Integrated Security=True");

        //Function that clears all text boxes
        public void ClearData()
        {
            fname_txt.Clear();
            lname_txt.Clear();
            username_txt.Clear();
            position_txt.Clear();
            salary_txt.Clear();
            search_id_txt.Clear();
        }

        //Method that loads the employee table data to the data grid
        public void LoadGrid()
        {
            //Query to retrieve all records from the Employee Table
            string query = "SELECT * FROM Employee";
            //Creating a new instance of SqlCommand
            SqlCommand cmd = new SqlCommand(query, conn);
            //Creating a new instance of Datatable
            DataTable dt = new DataTable();
            //Opening the connection
            conn.Open();
            //Executing the query, returns an SqlDataReader object
            SqlDataReader sdr = cmd.ExecuteReader();
            //Loading the returned SqlDataReader object to the DataTable object
            dt.Load(sdr);
            //Closing the connection
            conn.Close();
            //Loading the DataTable object to the UI grid to show
            datagrid.ItemsSource = dt.DefaultView;

        }

        //Method that would check if any of text box values are empty
        public bool ValidateInputData()
        {
            if(fname_txt.Text == string.Empty)
            {
                MessageBox.Show("First Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (lname_txt.Text == string.Empty)
            {
                MessageBox.Show("Last Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (username_txt.Text == string.Empty)
            {
                MessageBox.Show("Username is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (position_txt.Text == string.Empty)
            {
                MessageBox.Show("Position is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (salary_txt.Text == string.Empty)
            {
                MessageBox.Show("Salary is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        //Method that validates the ID text box for updating the record
        public bool ValidateIdTextBox()
        {
            if (search_id_txt.Text == string.Empty)
            {
                MessageBox.Show("Id is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        //Method that clears all text boxes
        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        //Method that inserts data into Employee Table
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            //Query to insert data into Employee table
            string query = "INSERT INTO Employee VALUES (@FirstName,@LastName,@UserName,@Position,@Salary)";
            //Checks if input data is validated, insert data if valid
            if(ValidateInputData())
            {
                try
                {
                    //Creating instance of SqlCommand
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //Setting command type
                    cmd.CommandType = CommandType.Text;
                    //Passing parameters
                    cmd.Parameters.AddWithValue("@FirstName", fname_txt.Text);
                    cmd.Parameters.AddWithValue("@LastName", lname_txt.Text);
                    cmd.Parameters.AddWithValue("@UserName", username_txt.Text);
                    cmd.Parameters.AddWithValue("@Position", position_txt.Text);
                    cmd.Parameters.AddWithValue("@Salary", salary_txt.Text);
                    //Opening connection
                    conn.Open();
                    //Executing query
                    cmd.ExecuteNonQuery();
                    //Closing connection
                    conn.Close();
                    //Calling LoadGrid method to load the data into data grid
                    LoadGrid();
                    MessageBox.Show("New employee added successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Clearing all text boxes
                    ClearData();
                } 
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Method that deletes a record
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string query = "DELETE FROM Employee WHERE ID = @ID";
            if (ValidateIdTextBox())
            {
                try
                {
                    //Creating instance of SqlCommand
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //Setting command type
                    cmd.CommandType = CommandType.Text;
                    //Passing parameters
                    cmd.Parameters.AddWithValue("@ID", search_id_txt.Text);
                    //Opening connection
                    conn.Open();
                    //Executing query
                    cmd.ExecuteNonQuery();
                    //Closing connection
                    conn.Close();
                    //Calling LoadGrid method to load the data into data grid
                    LoadGrid();
                    //To Do, Need to check if query is executed
                    MessageBox.Show("Employee deleted successfully", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Clearing all text boxes
                    ClearData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error Deleting employee" +ex.Message);
                } 
                finally
                {
                    conn.Close();
                }
            }     
        }

        //Method that updates a record
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE EMPLOYEE SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Position = @Position, Salary = @Salary WHERE ID = @ID"; 
            if (ValidateInputData() && ValidateIdTextBox())
            {
                try
                {
                    //Creating instance of SqlCommand
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //Setting command type
                    cmd.CommandType = CommandType.Text;
                    //Passing parameters
                    cmd.Parameters.AddWithValue("@FirstName", fname_txt.Text);
                    cmd.Parameters.AddWithValue("@LastName", lname_txt.Text);
                    cmd.Parameters.AddWithValue("@UserName", username_txt.Text);
                    cmd.Parameters.AddWithValue("@Position", position_txt.Text);
                    cmd.Parameters.AddWithValue("@Salary", salary_txt.Text);
                    cmd.Parameters.AddWithValue("@ID", search_id_txt.Text);
                    //Opening connection
                    conn.Open();
                    //Executing query
                    cmd.ExecuteNonQuery();
                    //Closing connection
                    conn.Close();
                    //Calling LoadGrid method to load the data into data grid
                    LoadGrid();
                    //To Do, Need to check if query is executed
                    MessageBox.Show("Employee updated successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Clearing all text boxes
                    ClearData();
                }
                catch(SqlException ex)
                {
                    MessageBox.Show("Error updating employee" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
