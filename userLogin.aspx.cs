using System;
using DBL;
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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void UserLoginBT_Click(object sender, EventArgs e)
        {
            string userId = Server.HtmlDecode(FødselNrTextBox.Text);

            // sjekke at user har task in 11 sifre
            if (userId.Length != 11)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vennligst skriv inn nøyaktig 11 sifre.');", true);
                return;
            }

            int voted = -1;

            DBL.Class1 DB = new DBL.Class1();
            //DataTable dt = DB.GetVoteByUserID(int.Parse(userId));
            DataTable dt = DB.GetVoteByUserID(userId);


            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT Voted FROM User_Data WHERE UserID = @UserID", conn);
            //    cmd.CommandType = CommandType.Text;

            //    cmd.Parameters.AddWithValue("@UserID", userId);

            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();

            if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                        voted = (int)row["Voted"];
                }

                if (voted == 0)
                {
                    //conn.Close();
                    //Create session login true
                    Session["userLoggedIn"] = FødselNrTextBox.Text;
                    Response.Redirect("UserVote.aspx");
                }

                if (voted == 1)
                {
                    //conn.Close();
                    Response.Redirect("viewVotes.aspx");
                }

                

                if (voted == -1)  
                {
                    DBL.Class1 DBInsertUserId = new Class1();
                    DBInsertUserId.InsertUseridWithNoVoted(userId);
                   

                    //Create session login true
                    Session["userLoggedIn"] = FødselNrTextBox.Text;
                    //conn.Close();
                    Response.Redirect("UserVote.aspx");
                }

            
        }
    }
}