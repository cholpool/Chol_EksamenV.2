using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using DBL;

namespace EksamenV._2_1_
{
    public partial class viewVotes : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            BindChart();
            BindChart2();
            
            if (!IsPostBack)
            {
                BindDropDownListKommu();
                BindTotaltVotesGrid();
            }
        }

        protected void kommuDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart3DropDown();
        }

        public DataTable GetVotes()
        {
            Class1 dbl = new Class1();
            DataTable dt = dbl.GetVotes();
            return dt;
        }

        private DataTable GetVotesByKid()
        {
            int Kid = int.Parse(kommuDropDownList.SelectedValue);

            Class1 dbl = new Class1();
            DataTable dt = dbl.GetVotesByKid(Kid);

            return dt;

            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("select COUNT(Vote_Data.Pid) AS stemmer, Parti.PartiNavn FROM Vote_Data INNER JOIN Parti ON Parti.Pid = Vote_Data.Pid where Kid = @Kid GROUP BY Parti.PartiNavn; ", conn);//@ betyr at det er et parameter
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@Kid", int.Parse(kommuDropDownList.SelectedValue));
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();
            //    conn.Close();
            //}
            //return dt;
        }

        private void BindChart()
        {
            Chart1.Series[0].XValueMember = "PartiNavn";
            Chart1.Series[0].XValueType = ChartValueType.Int32;//optional
            Chart1.Series[0].YValueMembers = "stemmer";
            Chart1.Series[0].ChartType = SeriesChartType.Bar;

            //chart datasource - call method GetVotes()
            Chart1.DataSource = GetVotes();
            Chart1.DataBind();
        }

        private void BindChart2()
        {
            Chart2.Series[0].XValueMember = "PartiNavn";
            Chart2.Series[0].XValueType = ChartValueType.Int32;//optional
            Chart2.Series[0].YValueMembers = "stemmer_på_prosent";
            Chart2.Series[0].ChartType = SeriesChartType.Pie;

            //chart datasource - call method GetVotes()
            Chart2.DataSource = GetVotesProsent();
            Chart2.DataBind();
        }

        private void BindChart3DropDown()
        {
            Chart3.Series[0].XValueMember = "PartiNavn";
            Chart3.Series[0].XValueType = ChartValueType.Int32;//optional
            Chart3.Series[0].YValueMembers = "stemmer";
            Chart3.Series[0].ChartType = SeriesChartType.Bar;
                 
            //cha3t datasource - call method GetVotes()
            Chart3.DataSource = GetVotesByKid();
            Chart3.DataBind();
        }

        private void BindTotaltVotesGrid()
        {
            totaltOversiktGV.DataSource= GetTotaltVotesSortByKommune();
            totaltOversiktGV.DataBind();
        }

        private DataTable GetTotaltVotesSortByKommune()
        {
            Class1 dbl = new Class1();
            DataTable dt = dbl.GetTotaltVotesSortByKommune();
            return dt;
            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT Kommune.Kommune, Parti.PartiNavn, COUNT(Vote_Data.Pid) AS stemmer " +
            //                                    "FROM Vote_Data " +
            //                                    "INNER JOIN Parti ON Parti.Pid = Vote_Data.Pid " +
            //                                    "INNER JOIN Kommune ON Kommune.Kid = Vote_Data.Kid " +
            //                                    "GROUP BY Kommune.Kommune, Parti.PartiNavn " +
            //                                    "ORDER BY Kommune.Kommune", conn);
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();
            //    conn.Close();
            //}
            //return dt;
        }

        private DataTable GetVotesProsent()
        {
            Class1 dbl = new Class1();
            DataTable dt = dbl.GetVotesProsent();
            return dt;
            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            //DataTable dt = new DataTable();
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT (COUNT(Vote_Data.Pid) * 100 / (SELECT COUNT(*) FROM Vote_Data)) AS stemmer_på_prosent, Parti.PartiNavn FROM Vote_Data INNER JOIN Parti ON Parti.Pid = Vote_Data.Pid GROUP BY Parti.PartiNavn;", conn);//@ betyr at det er et parameter
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    dt.Load(reader);
            //    reader.Close();
            //    conn.Close();
            //}
            //return dt;
        }

        private void BindDropDownListKommu()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();

            //Placeholder for dropdown2
            ListItem placeholderItem = new ListItem("Select Kommune", "-1");
            kommuDropDownList.Items.Add(placeholderItem);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Kommune", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            //loope gjennom datatable for å hente ut partinavn. lage et dropdownitem og putte navnet i det
            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem(row["Kommune"].ToString(), row["Kid"].ToString());//hente ut verdier
                kommuDropDownList.Items.Add(item);//legge item i lista
            }

            kommuDropDownList.DataBind();
        }

        
    }
}