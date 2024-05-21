using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EksamenV._2_1_
{
    public partial class Random : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerateVotes_Click(object sender, EventArgs e)
        {
            int numberOfVotes = 100;
            GenerateRandomVotes(numberOfVotes);
        }

        private void GenerateRandomVotes(int numberOfVotes)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                System.Random random = new System.Random();

                for (int i = 0; i < numberOfVotes; i++)
                {
                    int randomPid = random.Next(1, 6);
                    int randomKid = random.Next(1, 6);

                    SqlCommand cmd = new SqlCommand("INSERT INTO Vote_Data (Pid, Kid) VALUES (@Pid, @Kid)", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Pid", randomPid);
                    cmd.Parameters.AddWithValue("@Kid", randomKid);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}