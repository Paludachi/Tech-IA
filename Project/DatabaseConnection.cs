using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
namespace Project
{
    class DatabaseConnection
    {
        private string sql_string = "SELECT * FROM dbo.Planner";
        private string strCon;
        System.Data.SqlClient.SqlDataAdapter da_1;
        //string DB = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database1.mdf");
        //string conString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={DB};" +
                          //$"Integrated Security = True; Connect Timeout = 30";

        public string SQL
        {
            set
            {
                sql_string = value;
            }
        }

        public string connection_string
        {

            set { strCon = value; } 
        }

        public System.Data.DataSet GetConnection
        {
            get{return MyDataSet();}
        }

        public void UpdateDatabase(System.Data.DataSet ds)
        {
            System.Data.SqlClient.SqlCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder(da_1);
            cb.DataAdapter.Update(ds.Tables["Planner"]);
        }

        private System.Data.DataSet MyDataSet()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strCon);
            con.Open();
            System.Data.SqlClient.SqlDataAdapter da_1;
            da_1 = new System.Data.SqlClient.SqlDataAdapter("AllItemsPlanner", con);
            System.Data.DataSet dat_set = new System.Data.DataSet();
            da_1.Fill(dat_set, "Planner");
            con.Close();
            return dat_set;
        }

    }
}
