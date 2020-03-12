using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Project
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        
        BindingSource binder = new BindingSource();
        string conString;
        SqlConnection con;
        DatabaseConnection objConnect;
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {      
            con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("AllItemsNotes", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            con.Close();
            btnDelete.Enabled = true;
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql = null;
            sql = "insert into Notes ([Note]) values(@notes)";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = txtComment.Text;
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
            sql = "DELETE from Notes Where Note = @note";
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.AddWithValue("@note", txtComment.Text);
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string note;
            DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
            note = selectRow.Cells[0].Value.ToString();
            txtComment.Text = note;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guide: " + "\n\n" + "Back: Takes you back to the previous page" + "\n\n" + "View: The view button allows you to view all records you have entered. If you click on the header of a record, the data will appear in the data box to the right." + "\n\n" + "Save: Once you enter information in the correct databox, press save. It will appear in the table the next time you enter the page or pick 'View'" + "\n\n" + "Delete: When you no longer want a record to appear in the data table, click on the header of the record after clicking view. Then click Delete. The data will dissapear when you next open the form or click 'View'");
        }

        private void Form3_Load(object sender, EventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtComment.ReadOnly = false;
            btnView.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
        }
    }
}
