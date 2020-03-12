using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        BindingSource binder = new BindingSource();
        string conString;
        SqlConnection con;
        DatabaseConnection objConnect;
      

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                string DB = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database1.mdf");
                conString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={DB};" +
                              $"Integrated Security = True; Connect Timeout = 30";
                objConnect.connection_string = conString;
                objConnect.SQL = Properties.Settings.Default.SQL;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand("AllItemsPlanner", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = binder;
                binder.DataSource = dataTable;
                con.Close();
            btnDelete.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtDay.Text = Convert.ToString(dateTimePicker1.Value.Day);
            txtMonth.Text = Convert.ToString(dateTimePicker1.Value.Month);
            txtYear.Text = Convert.ToString(dateTimePicker1.Value.Year);
            txtComment.Clear();
        }
        

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string day;
            string month;
            string year;
            string comment;
            

            DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
            year = selectRow.Cells[2].Value.ToString();
            month = selectRow.Cells[1].Value.ToString();
            day = selectRow.Cells[0].Value.ToString();
            comment = selectRow.Cells[3].Value.ToString();
            

            txtDay.Text = day;
            txtMonth.Text = month;
            txtYear.Text = year;
            txtComment.Text = comment;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            string sql = null;
            sql = "insert into Planner ([Day], [Month], [Year], [Comment]) values(@day, @month, @year, @comment)";
            using (con)
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@day", SqlDbType.Int).Value = txtDay.Text;
                        cmd.Parameters.Add("@month", SqlDbType.Int).Value = txtMonth.Text;
                        cmd.Parameters.Add("@year", SqlDbType.Int).Value = txtYear.Text;
                        cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = txtComment.Text;
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded > 0)
                            MessageBox.Show("Data Added!!");
                        else

                            MessageBox.Show("No Data inserted");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
            this.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            string SQL = null;
            SQL = "DELETE from Planner Where [Day] = @day and [Month] = @month and [Year] = @year and [Comment] = @comment ";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@day", txtDay.Text);
                        cmd.Parameters.AddWithValue("@month", txtMonth.Text);
                        cmd.Parameters.AddWithValue("@year", txtYear.Text);
                        cmd.Parameters.AddWithValue("@comment", txtComment.Text);
                        int rowsDeleted = cmd.ExecuteNonQuery();
                        if (rowsDeleted > 0)
                            MessageBox.Show("Data Deleted!!");
                        else

                            MessageBox.Show("No Data removed");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guide: " + "\n\n" + "Back: Takes you back to the previous page" + "\n\n" + "View: The view button allows you to view all records you have entered. If you click on the header of a record, the data will appear in the data boxes to the right." + "\n\n" +"Save: Once you enter information in the correct databox, press save. It will appear in the table the next time you enter the page or pick 'View'" + "\n\n" + "Delete: When you no longer want a record to appear in the data table, click on the header of the record after clicking view. Then click Delete. The data will dissapear when you next open the form or click 'View'");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            string day;
            string month;
            string year;
            string comment;


            DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
            year = selectRow.Cells[2].Value.ToString();
            month = selectRow.Cells[1].Value.ToString();
            day = selectRow.Cells[0].Value.ToString();
            comment = selectRow.Cells[3].Value.ToString();


            txtDay.Text = day;
            txtMonth.Text = month;
            txtYear.Text = year;
            txtComment.Text = comment;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtDay.Clear();
            txtMonth.Clear();
            txtYear.Clear();
            txtComment.Clear();
            txtDay.ReadOnly = false;
            txtMonth.ReadOnly = false;
            txtYear.ReadOnly = false;
            txtComment.ReadOnly = false;
            btnView.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
        }
    } 
}