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
        Services services = new Services();
        public MainWindow()
        {
            //Initializing all the UI components
            InitializeComponent();
            //Load the employee table data to the data grid
            LoadGrid();;
        }


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

        //Methods that loads the employee table data to the data grid
        public void LoadGrid()
        {
            DataTable dt = new DataTable();
            services.OpenConnection();
            SqlDataReader reader = services.GetEmployees();
            dt.Load(reader);
            datagrid.ItemsSource = dt.DefaultView;
            services.CloseConnection();
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
            string first_name = fname_txt.Text;
            string last_name = lname_txt.Text;
            string username = username_txt.Text;
            string position = position_txt.Text;
            string salary = salary_txt.Text;
            //Checks if input data is validated, insert data if valid
            if(ValidateInputData())
            {
                services.OpenConnection();
                services.InsertEmployee(first_name, last_name, username, position, salary);
                services.CloseConnection();
                //Calling LoadGrid method to load the data into data grid
                LoadGrid();
                MessageBox.Show("New employee added successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                //Clearing all text boxes
                ClearData();
            }
        }

        //Method that deletes a record
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = search_id_txt.Text;
            if (ValidateIdTextBox())
            {
                services.OpenConnection();
                services.DeleteEmployee(id);
                services.CloseConnection();
                LoadGrid();
                MessageBox.Show("Employee deleted successfully", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearData();                       
            }     
        }

        //Method that updates a record
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string first_name = fname_txt.Text;
            string last_name = lname_txt.Text;
            string username = username_txt.Text;
            string position = position_txt.Text;
            string salary = salary_txt.Text;
            string id = search_id_txt.Text;
            if (ValidateInputData() && ValidateIdTextBox())
            {
                services.OpenConnection();
                services.UpdateEmployee(first_name, last_name, username, position, salary, id);
                services.CloseConnection();
                LoadGrid();
                MessageBox.Show("Employee updated successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearData();
            }
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            services.OpenConnection();
            services.ExportEmployees();
            services.CloseConnection();
        }
    }
}
