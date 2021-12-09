using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class TableStudentsManager
    {
        private static DbSchoolConnection sqlConnection = new DbSchoolConnection();
        private SqlCommand command= new SqlCommand();

        public DataTable getStudents() 
        {
            DataTable table = new DataTable();

            command.Connection = sqlConnection.OpenConnection();
            command.CommandText = "GetStudents";
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader read = command.ExecuteReader();
            table.Load(read);

            sqlConnection.CloseConnection();

            return table;
        }

        public DataTable getStudents(string id)
        {
            DataTable table = new DataTable();

            command.Connection = sqlConnection.OpenConnection();
            command.CommandText = "GetStudentsById";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StudentId", id);

            SqlDataReader read = command.ExecuteReader();
            table.Load(read);

            command.Parameters.Clear();
            sqlConnection.CloseConnection();

            return table;
        }

        public void AddStudent(string id, string name, string email, string phone_number) 
        {
            command.Connection = sqlConnection.OpenConnection();
            command.CommandText = "AddStudent";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@StudentId", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@phone_number", phone_number);

            command.ExecuteNonQuery();

            command.Parameters.Clear();
            sqlConnection.CloseConnection();
        }
    }
}
