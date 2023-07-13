
using System.Data.SqlClient;

namespace MVC_Achitecture;
public class Connection
{
    private static string _connectionString =
    "Data Source=LAPTOP-55C9MTUJ; " +
    "Database=db_perusahaan_abc;" +
    "Integrated Security=True;" +
    "Connect Timeout=30;";

    private static SqlConnection _connection;

    public static SqlConnection Get()
    {
        try 
        { 
            _connection = new SqlConnection(_connectionString);
            return _connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }

    }
    
}

