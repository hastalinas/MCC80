using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace DatabaseConnectivity;

public class Program
{
    private static string _connectionString =
        "Data Source=LAPTOP-55C9MTUJ; " +
        "Database=db_perusahaan_abc;" +
        "Integrated Security=True;" +
        "Connect Timeout=30;";


    private static SqlConnection _connection;
    public static void Main(string[] args)
    {
        Menu.MenuUtama();
        Console.Write("Input : ");
        string choice = Console.ReadLine();
        Console.Clear();

        switch (choice)
        {
            case "1":
                Console.WriteLine("=== Menu Tabel Region ===");
                Menu.MenuCrud();
                Console.Write("input : ");
                string input1 = Console.ReadLine();
                Regions.InsertRegion(input1, _connectionString);
                break;
            case "2":
                Console.WriteLine("=== Menu Tabel Country ===");
                Menu.MenuCrud();
                Console.Write("Input: ");
                break;
            case "3":
                Console.WriteLine("=== Menu Tabel Location ===");
                Menu.MenuCrud();
                break;
            case "4":
                Console.WriteLine("=== Menu Tabel Departement ===");
                Menu.MenuCrud();
                break;
            case "5":
                Console.WriteLine("=== Menu Tabel Employee ===");
                Menu.MenuCrud();
                break;
            case "6":
                Console.WriteLine("=== Menu Tabel Job ===");
                Menu.MenuCrud();
                break;
            case "7":
                Console.WriteLine("=== Menu Tabel History ===");
                Menu.MenuCrud();
                break;
            case "8":
                Console.WriteLine("Keluar dari program");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        //GetRegions();

        //Insert Region dengan Singapura
        //InsertRegion(name: "Singapura") ;

        //Ubah Region dengan id 9 ke Bangkok
        //int regionId = 9;
        //string regionName = "Bangkok";
        //UpdateRegion(regionId,regionName);

        //Hapus id
        //DeleteRegions(id: 10) ;
        //GetById(1);


        /*        _connection = new SqlConnection(_connectionString);

                try
                {
                    _connection.Open();
                    Console.WriteLine("Connection succesfull.");
                    _connection.Close();
                }
                catch
                {
                    Console.WriteLine("Error connecting to databasse");
                }*/
    }
}

public class Menu
{
    public static void MenuUtama()
    {
        Console.WriteLine("SHERINA ERIA HASTALINA");
        Console.WriteLine("======= MENU DATABASE HR =======");
        Console.WriteLine("1. Region");
        Console.WriteLine("2. Country");
        Console.WriteLine("3. Location");
        Console.WriteLine("4. Departement");
        Console.WriteLine("5. Employee");
        Console.WriteLine("6. Job");
        Console.WriteLine("7. History");
        Console.WriteLine("===============================");
    }

    public static void MenuCrud()
    {
        Console.WriteLine("1. Create");
        Console.WriteLine("2. Update");
        Console.WriteLine("3. Delete");
        Console.WriteLine("4. Get By Id");
        Console.WriteLine("5. Get All");
    }
}

public class Regions
{
    //GET ALL
    public static void GetRegions(string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM regions";

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No regions found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

    // INSERT REGION
    public static void InsertRegion(string name, string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO regions VALUES (@ujang)";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@ujang";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("insert succes");
            }
            else
            {
                Console.WriteLine("insert failed");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to database");
        }
    }


    // UPDATE REGION
    public static void UpdateRegion(int id, string name, string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Update regions set name = @Name where id = @Id";

        //Set paramaeter value
        sqlCommand.Parameters.AddWithValue("@Id", id);
        sqlCommand.Parameters.AddWithValue("Name", name);
        try
        {
            _connection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Console.WriteLine("Region updated succesfully.");
            }
            else
            {
                Console.WriteLine("No region found or no change made.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

    }

    // DELETE REGION
    public static void DeleteRegions(int id, string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Delete from regions where id = (@Id)";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Delete succes");
            }
            else
            {
                Console.WriteLine("Delete failed");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine("Error! " + ex.Message);
        }
    }

    // GET BY ID REGION
    public static void GetById(int id, string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT name FROM regions WHERE id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {

            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //Console.WriteLine("ID: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(0));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No regions found.");
            }

            reader.Close();
            //_connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!" + ex.Message);
        }
    }

}


public class Country
{
    // Get all countries
    public static void GetCountry(string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM countries";

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No regions found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

    // Insert Country
    public static void InsertCountry(string id, string name, int region_id, string _connectionString)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO countries (id, name, region_id) VALUES (@id, @name, @region_id)";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegId = new SqlParameter();
            pRegId.ParameterName = "@region_id";
            pRegId.SqlDbType = SqlDbType.Int;
            pRegId.Value = region_id;
            sqlCommand.Parameters.Add(pRegId);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("insert succes");
            }
            else
            {
                Console.WriteLine("insert failed");
            }
            transaction.Commit();
            _connection.Close();
        }
        catch
        {
            transaction.Rollback();
            Console.WriteLine("Error connecting to database");
        }

    }
}

public class Location
{

}

public class Departemen
{

}

public class Employee
{

}

public class Job
{

}

public class History
{

}





