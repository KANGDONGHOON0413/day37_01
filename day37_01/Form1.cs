using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace day37_01
{
    public partial class Form1 : Form
    {
        SqlDataAdapter DA;
        SqlConnection conn;
        DataSet Dset;
        string connString = "server = .\\SQLEXPRESS; database = test; uid = sa; password = alencia";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            Dset = new DataSet();
            DA = new SqlDataAdapter("SELECT * FROM testTable02", conn);
            DA.Fill(Dset, "testTable02");
            dataGridView1.DataSource = Dset.Tables[0];
                }

        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            conn.Open();
            DA.DeleteCommand = new SqlCommand("DELETE FROM [testTable02] WHERE Nid = @Nid");
            DA.DeleteCommand.Connection = conn;
            DA.DeleteCommand.Parameters.Add("@Nid", SqlDbType.Int, 12, "Nid");
            DA.DeleteCommand.Parameters["@Nid"].Value = int.Parse(INPUT_Delete.Text);
            DA.DeleteCommand.ExecuteNonQuery();

            Dset.Clear();
            DA.Fill(Dset, "testTable02");
            dataGridView1.DataSource = Dset.Tables[0];
            conn.Close();
            DA.DeleteCommand.Dispose();
        }
    }
}
