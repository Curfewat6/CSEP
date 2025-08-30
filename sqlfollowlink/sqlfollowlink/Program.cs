using System;
using System.Data.SqlClient;

namespace sqlfollowlink
{
    class Program
    {
        static void Main(string[] args)
        {
            String sqlServer = "sql11"; // THIS IS THE HOSTNAME OF THE SQL SERVER YOU HAVE A FOOTHOLD IN
            String database = "master";

            String conString = "Server = " + sqlServer + "; Database = " + database + "; Integrated Security = True;";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                Console.WriteLine("Auth success!");
            }
            catch
            {
                Console.WriteLine("Auth failed");
                Environment.Exit(0);
            }

            String execCmd = "EXEC sp_linkedservers;";

            SqlCommand command = new SqlCommand(execCmd, con);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("Linked SQL server: " + reader[0]);
            }
            reader.Close();

            con.Close();
        }
    }
}