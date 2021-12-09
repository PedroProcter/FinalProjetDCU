using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class DbSchoolConnection
    {
        private static string sqlConnectionString;
        private SqlConnection sqlConnection;

        public DbSchoolConnection() {
            sqlConnectionString = "Data Source=localhost;Initial Catalog=School;Integrated Security=True";
            sqlConnection = new SqlConnection(sqlConnectionString);
        }

        public SqlConnection OpenConnection() {

            if (sqlConnection.State == ConnectionState.Closed) 
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }

        public SqlConnection CloseConnection() {

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            return sqlConnection;
        }
    }   
}
