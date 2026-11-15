using Microsoft.Data.SqlClient;

namespace RegistroDeJugadoresDeBaloncesto2.Data
{
    public class DatabaseConnection
    {
        private string connectionString = "Server=localhost;Database=RegistroJugadoresDB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}



