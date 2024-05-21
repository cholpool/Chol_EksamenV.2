using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class Class1
    {
        public DataTable GetVoteByUserID(string userId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Voted from User_Data where UserID=@userId", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@userId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            return dt;
        }

        public void InsertUseridWithNoVoted(string userId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO User_Data (UserID, Voted) VALUES (@UserID, '0')", conn);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@UserID", userId);
                cmd2.ExecuteNonQuery();
                conn.Close();
            }


        }

        public DataTable GetAllKommuNavn()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
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
            return dt;
        }

        public DataTable GetAllPartiInfo()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Parti", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            return dt;
        }

        public void InsertVoteData(int Pid, int Kid, string UserId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Vote_Data (Pid, Kid) VALUES (@Pid, @Kid); " +
                                                "UPDATE User_Data SET Voted = '1' WHERE UserID = @UserID", conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Pid", Pid);
                cmd.Parameters.AddWithValue("@Kid", Kid);
                cmd.Parameters.AddWithValue("@UserID", UserId);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable GetVotes()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(Vote_Data.Pid) AS stemmer, Parti.PartiNavn FROM Vote_Data inner join Parti on Parti.Pid = Vote_Data.Pid GROUP BY Parti.PartiNavn", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            return dt;
        }

        public DataTable GetVotesByKid(int Kid)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(Vote_Data.Pid) AS stemmer, Parti.PartiNavn FROM Vote_Data INNER JOIN Parti ON Parti.Pid = Vote_Data.Pid where Kid = @Kid GROUP BY Parti.PartiNavn; ", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Kid", Kid);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            return dt;
        }

        public DataTable GetTotaltVotesSortByKommune()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Kommune.Kommune, Parti.PartiNavn, COUNT(Vote_Data.Pid) AS stemmer " +
                                                "FROM Vote_Data " +
                                                "INNER JOIN Parti ON Parti.Pid = Vote_Data.Pid " +
                                                "INNER JOIN Kommune ON Kommune.Kid = Vote_Data.Kid " +
                                                "GROUP BY Kommune.Kommune, Parti.PartiNavn " +
                                                "ORDER BY Kommune.Kommune", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            return dt;
        }

        public DataTable GetVotesProsent()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT (COUNT(Vote_Data.Pid) * 100 / (SELECT COUNT(*) FROM Vote_Data)) AS stemmer_på_prosent, Parti.PartiNavn FROM Vote_Data INNER JOIN Parti ON Parti.Pid = Vote_Data.Pid GROUP BY Parti.PartiNavn;", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            return dt;
        }
    }
}

