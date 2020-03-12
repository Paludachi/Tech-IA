using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        BindingSource binder = new BindingSource();
        DatabaseConnection objConnect;
        string conString;

        DataSet ds;
        DataRow dRow;

        int MaxRows;
        int inc = 0;

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.IDB;

                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                objConnect.connection_string = conString;
                objConnect.SQL = Properties.Settings.Default.SQL;
                NavigateRecords();

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }
        private void NavigateRecords()
        {

            dRow = ds.Tables[0].Rows[inc];

            txtFirstName.Text = dRow.ItemArray.GetValue(1).ToString();
            txtLastName.Text = dRow.ItemArray.GetValue(2).ToString();
            txtObject.Text = dRow.ItemArray.GetValue(3).ToString();
            txtDue.Text = dRow.ItemArray.GetValue(4).ToString();

        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            string conString;
            conString = Properties.Settings.Default.IDB;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("AllItems", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = binder;
            binder.DataSource = dataTable;
            con.Close();
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
            oID = selectRow.Cells[3].Value.ToString();
            fName = selectRow.Cells[2].Value.ToString();
            gName = selectRow.Cells[1].Value.ToString();
            sdue = selectRow.Cells[4].Value.ToString();
            
            txtObject.Text = oID;
            txtLastName.Text = fName;
            txtFirstName.Text = gName;
            txtDue.Text = sdue;


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fName = txtLastName.Text;
            string gName = txtFirstName.Text;
            string sdue = txtDue.Text;
            string objectID = txtObject.Text;
            string conString;
            conString = Properties.Settings.Default.IDB;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UpdateDatabase", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Fam", fName));
            cmd.Parameters.Add(new SqlParameter("@Giv", gName));
            cmd.Parameters.Add(new SqlParameter("@Obj", objectID));
            cmd.Parameters.Add(new SqlParameter("@Due", sdue));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated:");
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtObject.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtDue.Clear();
            NavigateRecords();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            DataRow row = ds.Tables[0].NewRow();
            row[1] = txtFirstName.Text;
            row[2] = txtLastName.Text;
            row[3] = txtObject.Text;
            row[4] = txtDue.Text;

            ds.Tables[0].Rows.Add(row);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}