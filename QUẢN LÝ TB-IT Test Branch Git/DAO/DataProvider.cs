using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CONNECTION;
using ChuoiKetNoi;
namespace DAO
{
  
   public class DataProvider
    {

        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        public string strcon = "";
        
      
                     
        public DataTable ExecuteQuery(string query, object[] para = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                if (para != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(data);
                sqlcon.Close();

            }
            return data;

        }

        public int ExecuteNonQuery(string query, object[] para = null)
        {
            int data = 0;
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                if (para != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }

                }

                data = cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] para = null)
        {
            object data = null;
            using (SqlConnection sqlcon = new SqlConnection(strcon))
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                if (para != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }

                }
                data = cmd.ExecuteScalar();
                sqlcon.Close();

            }
            return data;

        }
    }

}
