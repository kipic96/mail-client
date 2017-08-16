using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace PublicTransportSystem
{
    class TimetableDatabase
    {
        static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);

        public TimetableDatabase()
        {
            ConnectToDatabase();            
        }

        void ConnectToDatabase()
        {
            using (_connection)
            {
                _connection.Open();
                UseDatabase();
            }
        }

        void UseDatabase()
        {
            SqlCommand command2 = new SqlCommand("DELETE FROM city WHERE name = @city", _connection);
            SqlCommand command = new SqlCommand("SELECT * FROM city", _connection);
            command2.Parameters.AddWithValue("@city", "Krakow");
            command2.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            
            using (reader)
            {
                while (reader.Read())
                {
                    MessageBox.Show(String.Format("id = {0}, name = {1}", reader[0].ToString(), reader[1].ToString()));
                }
            }
            MessageBox.Show("Thats all, mate");
        }
    }
}
