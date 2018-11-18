using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUDCSHARPSQLSERVER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //initialize all classes
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

        //declaring variables
            string query;
            int  result;
            int userid;

        //create a method for clearing all textbox
            private void clear()
            {
                userid =0;
                txtname.Clear();
                txtuname.Clear();
                txtpass.Clear();
                cborole.Text = "Select"; 
            }
         
        private void Form1_Load(object sender, EventArgs e)
        {
            //set a connection between SQL server and Visual C#
            conn.ConnectionString = "Data Source=.\\SQLEXPRESS;database=myFirstCruddb;trusted_connection=true;";
           //AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath + "\\");

            //conn.ConnectionString = "Data Source=.\\SQLEXPRESS;" +
            //"User Instance=true;" +
            //"Integrated Security=true;" +
            //"AttachDbFilename=" + Application.StartupPath + "\\myFirstCruddb.mdf;";

        }

        //Creating Method
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                //opening connection
                conn.Open();
                //create an insert query;
                query = "INSERT INTO tblcrud (NAME,UNAME,PASS,UTYPE) VALUES('" + txtname.Text + "','" + txtuname.Text + "','" + txtpass.Text + "','" + cborole.Text + "')";
                //it holds the data to be executed.
                cmd.Connection = conn;
                cmd.CommandText = query;
                //execute the data.
                result = cmd.ExecuteNonQuery();
                //validate if the result of the executed query.
                if (result > 0)
                {
                    
                    MessageBox.Show("Data has been saved in the SQL database");
                    //calling a method
                    btnreload_Click(sender, e);

                }
                else
                {
                    MessageBox.Show("SQL QUERY ERROR");
                }
                //closing connection
                conn.Close();
            
            }
            catch(Exception ex)//catch exeption
            {
                //displaying error message.
                MessageBox.Show(ex.Message);
            }
            
        }

        //delete method
        private void btndel_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                //delete query
                query = "DELETE FROM tblcrud WHERE ID = " + dtgList.CurrentRow.Cells["ID"].FormattedValue;
                //it holds the data to be executed.
                cmd.Connection = conn;
                cmd.CommandText = query;
                //execute the data.
                result = cmd.ExecuteNonQuery();
                //validate if the result of the executed query.

                if (result > 0)
                {
                    MessageBox.Show("Data has been deleted in the SQL database");
                    //calling a method
                    btnreload_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("SQL QUERY ERROR");
                }
                
            }
          
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ); 
            }
            finally
            { 
                conn.Close();
            }

        }
        //diplay the specific data from the datagridview to the textbox 
        private void dtgList_DoubleClick(object sender, EventArgs e)
        {
            userid =  Int32.Parse(dtgList.CurrentRow.Cells["Id"].FormattedValue.ToString());
            txtname.Text = dtgList.CurrentRow.Cells["Name"].FormattedValue.ToString();
            txtuname.Text = dtgList.CurrentRow.Cells["Username"].FormattedValue.ToString();
            txtpass.Text = dtgList.CurrentRow.Cells["PASS"].FormattedValue.ToString();
            cborole.Text = dtgList.CurrentRow.Cells["Role"].FormattedValue.ToString();       
        }

        //load method
        private void btnreload_Click(object sender, EventArgs e)
        {
            try
            {
               //create a query for retrieving data in the database.
                query = "SELECT ID as 'Id',NAME as 'Name',UNAME as 'Username', UTYPE as 'Role',PASS FROM tblcrud";
                //initialize new Sql commands
                cmd = new SqlCommand();
                //hold the data to be executed.
                cmd.Connection = conn;
                cmd.CommandText = query;
                //initialize new Sql data adapter
                da = new SqlDataAdapter();
                //fetching query in the database.
                da.SelectCommand = cmd;
                //initialize new datatable
                dt = new DataTable();
                //refreshes the rows in specified range in the datasource. 
                da.Fill(dt);
                //set the data that to be display in the datagridview
                dtgList.DataSource = dt;
                //hide the password column in the datagridview 
                dtgList.Columns["PASS"].Visible  = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                da.Dispose();
                 
            }
            userid = 0;

        }
        //update method
        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = "UPDATE tblcrud SET NAME='" + txtname.Text + "',UNAME='" + txtuname.Text + "',PASS='" + txtpass.Text + "' ,UTYPE='" + cborole.Text + "' WHERE ID = " + userid;
                cmd.Connection = conn;
                cmd.CommandText = query;
                result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data has been updated in the SQL database");
                    //calling a method
                    btnreload_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("SQL QUERY ERROR");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        //new method
        private void btnnew_Click(object sender, EventArgs e)
        {
            //calling a method
            clear();
        }

     
        
    }
}
