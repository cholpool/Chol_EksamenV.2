using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBL;

namespace EksamenV._2_1_
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)//new request page, not triggered on button click - postback
            {
                BindDropDownListParti();
                BindDropDownListKommu();
            }

            if (Session["userLoggedIn"] == null)
                Response.Redirect("userlogin.aspx");

            //vote 0 - select check if person vote is 0 times
            string persNr = Session["userLoggedIn"].ToString();
            int voted = GetVotedByUserId(persNr);

            if (voted == 1)//has voted already
                Response.Redirect("viewvotes.aspx");
        }

        private void BindDropDownListKommu()
        {
            Class1 dbl = new Class1();
            DataTable dt = dbl.GetAllKommuNavn();

            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT * from Kommune", conn);//@ betyr at det er et parameter
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();
            //    conn.Close();
            //}

            //loope gjennom datatable for å hente ut partinavn. lage et dropdownitem og putte navnet i det
            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem(row["Kommune"].ToString(), row["Kid"].ToString());//hente ut verdier
                DropDownListKommu.Items.Add(item);//legge item i lista
            }

            //DropDownListParti.DataSource= dt;
            DropDownListKommu.DataBind();
        }

        private void BindDropDownListParti()//dette blir en select *. det som returneres skal bindes til dropdown 
        {
            Class1 dbl = new Class1();
            DataTable dt = dbl.GetAllPartiInfo();

            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT * from Parti", conn);//@ betyr at det er et parameter
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();
            //    conn.Close();
            //}

            //loope gjennom datatable for å hente ut partinavn. lage et dropdownitem og putte navnet i det
            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem(row["PartiNavn"].ToString(), row["Pid"].ToString());//hente ut verdier
                DropDownListParti.Items.Add(item);//legge item i lista
            }

            //DropDownListParti.DataSource= dt;
            DropDownListParti.DataBind();
        }

        public int GetVotedByUserId(string userId)
        {
            int voted = -1;

            Class1 dbl = new Class1();
            DataTable dt = dbl.GetVoteByUserID(userId);
            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT Voted FROM User_Data WHERE UserID = @userId", conn);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@userId", userId);

            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();
            //    conn.Close();
            //}

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                    voted = (int)row["Voted"];
                return voted;
            }

            if (dt.Rows.Count == 0)
                Response.Redirect("userlogin.aspx");

            return voted;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            int Pid = int.Parse(DropDownListParti.SelectedValue);
            int Kid = int.Parse(DropDownListKommu.SelectedValue);
            string userID = Session["userLoggedIn"].ToString();

            Class1 dbl = new Class1();
            dbl.InsertVoteData(Pid, Kid, userID);

            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("INSERT INTO Vote_Data (Pid, Kid) VALUES (@Pid, @Kid); " +
            //                                    "UPDATE User_Data SET Voted = '1' WHERE UserID = @UserID", conn);
            //    cmd.CommandType = CommandType.Text;

            //    cmd.Parameters.AddWithValue("@Pid", int.Parse(DropDownListParti.SelectedValue));
            //    cmd.Parameters.AddWithValue("@Kid", int.Parse(DropDownListKommu.SelectedValue));
            //    cmd.Parameters.AddWithValue("@UserID", Session["userLoggedIn"].ToString());

            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //}

            Response.Redirect("viewvotes.aspx");
        }




    }

}
