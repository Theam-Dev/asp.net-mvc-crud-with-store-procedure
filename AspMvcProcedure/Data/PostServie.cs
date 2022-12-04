using AspMvcProcedure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AspMvcProcedure.Data
{
    public class PostServie
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constring);
        }
        public bool Add(Post pmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("post_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CMD", "INSERT");
            cmd.Parameters.AddWithValue("@TITLE", pmodel.Title);
            cmd.Parameters.AddWithValue("@BODY", pmodel.Body);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<Post> Gets()
        {
            connection();
            List<Post> personlist = new List<Post>();

            SqlCommand cmd = new SqlCommand("post_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CMD","GET");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                personlist.Add(
                    new Post
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        Body = Convert.ToString(dr["Body"])
                    });
            }
            return personlist;
        }
        public bool Update(Post pmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("post_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CMD", "UPDATE");
            cmd.Parameters.AddWithValue("@Id", pmodel.Id);
            cmd.Parameters.AddWithValue("@TITLE", pmodel.Title);
            cmd.Parameters.AddWithValue("@BODY", pmodel.Body);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool Delete(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("post_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CMD", "DELETE");
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}