using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        BindingSource binder = new BindingSource();
        DatabaseConnection objConnect;
        string conString;

        DataSet ds;
        DataTable dt = new DataTable("AllItemsPlanner");
        

        DataRow dRow;

        int MaxRows;
        int inc = 0;

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.IDB;

                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                objConnect.connection_string = conString;
                objConnect.SQL = Properties.Settings.Default.SQL;


            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string conString;
            conString = Properties.Settings.Default.IDB;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("AllItemsPlanner", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = binder;
            binder.DataSource = dataTable;
            con.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtDay.Text = Convert.ToString(dateTimePicker1.Value.Day);
            if (dateTimePicker1.Value.Month == 1)
            {
                txtMonth.Text = "January";
            }
            else if (dateTimePicker1.Value.Month == 2)
            {
                txtMonth.Text = "February";
            }
            else if (dateTimePicker1.Value.Month == 3)
            {
                txtMonth.Text = "March";
            }
            else if (dateTimePicker1.Value.Month == 4)
            {
                txtMonth.Text = "April";
            }
            else if (dateTimePicker1.Value.Month == 5)
            {
                txtMonth.Text = "May";
            }
            else if (dateTimePicker1.Value.Month == 6)
            {
                txtMonth.Text = "June";
            }
            else if (dateTimePicker1.Value.Month == 7)
            {
                txtMonth.Text = "July";
            }
            else if (dateTimePicker1.Value.Month == 8)
            {
                txtMonth.Text = "August";
            }
            else if (dateTimePicker1.Value.Month == 9)
            {
                txtMonth.Text = "September";
            }
            else if (dateTimePicker1.Value.Month == 10)
            {
                txtMonth.Text = "October";
            }
            else if (dateTimePicker1.Value.Month == 11)
            {
                txtMonth.Text = "November";
            }
            else if (dateTimePicker1.Value.Month == 12)
            {
                txtMonth.Text = "December";
            }
            txtYear.Text = Convert.ToString(dateTimePicker1.Value.Year);
            txtComment.Clear();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            string day;
            string month;
            string year;
            string comment;

            DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
            year = selectRow.Cells[3].Value.ToString();
            month = selectRow.Cells[2].Value.ToString();
            day = selectRow.Cells[1].Value.ToString();
            comment = selectRow.Cells[4].Value.ToString();

            txtDay.Text = day;
            txtMonth.Text = month;
            txtYear.Text = year;
            txtComment.Text = comment;
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

            DataRow row = ds.Tables["Planner"].NewRow();
            row[2] = Convert.ToString(dateTimePicker1.Value.Month);
            row[3] = Convert.ToString(dateTimePicker1.Value.Year);
            row[4] = txtComment.Text;
            row[1] = Convert.ToString(dateTimePicker1.Value.Day);

            ds.Tables["Planner"].Rows.Add(row);
            try
            {
                objConnect.UpdateDatabase(ds);

                MaxRows = MaxRows + 1;
                inc = MaxRows - 1;

                MessageBox.Show("Database updated");

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                

            }
        }
    }
}