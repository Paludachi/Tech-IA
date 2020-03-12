using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        
        BindingSource binder = new BindingSource();
        string conString;
        SqlConnection con;
        DatabaseConnection objConnect;
        


        private void Form4_Load(object sender, EventArgs e)
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
        

        private void BtnView_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("AllItems", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            con.Close();
            btnDelete.Enabled = true;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string oID;
            string fName;
            string gName;
            string sdue;

            DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
            oID = selectRow.Cells[2].Value.ToString();
            fName = selectRow.Cells[1].Value.ToString();
            gName = selectRow.Cells[0].Value.ToString();
            sdue = selectRow.Cells[3].Value.ToString();
            
            txtObject.Text = oID;
            txtLastName.Text = fName;
            txtFirstName.Text = gName;
            txtDue.Text = sdue;


        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {          
            string sql = null;
            sql = "insert into Students ([FirstName], [FamilyName], [ObjectName], [DateDue]) values(@firstName, @lastName, @object, @due)";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = txtFirstName.Text;
                        cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = txtLastName.Text;
                        cmd.Parameters.Add("@object", SqlDbType.NVarChar).Value = txtObject.Text;
                        cmd.Parameters.Add("@due", SqlDbType.Date).Value = txtDue.Text;
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
            string sql = null;
            sql = "DELETE from Students Where [FirstName] = @firstName and [ObjectName] = @object";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@object", txtObject.Text);
                        cmd.Parameters.AddWithValue("@due", txtDue.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guide: " + "\n\n" + "Back: Takes you back to the previous page" + "\n\n" + "View: The view button allows you to view all records you have entered. If you click on the header of a record, the data will appear in the data boxes to the right." + "\n\n" + "Add: Once you enter information in the correct databoxes, press save. It will appear in the table the next time you enter the page or pick 'View'" + "\n\n" + "Delete: When you no longer want a record to appear in the data table, click on the header of the record after clicking view. Then click Delete. The data will dissapear when you next open the form or click 'View'");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            //Proof of concept
            string oID;
            string fName;
            string gName;
            string sdue;

            DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
            oID = selectRow.Cells[2].Value.ToString();
            fName = selectRow.Cells[1].Value.ToString();
            gName = selectRow.Cells[0].Value.ToString();
            sdue = selectRow.Cells[3].Value.ToString();

            txtObject.Text = oID;
            txtLastName.Text = fName;
            txtFirstName.Text = gName;
            txtDue.Text = sdue;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtObject.Clear();
            txtDue.Clear();
            txtFirstName.ReadOnly = false;
            txtLastName.ReadOnly = false;
            txtObject.ReadOnly = false;
            txtDue.ReadOnly = false;
            btnView.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
        }
    }
}