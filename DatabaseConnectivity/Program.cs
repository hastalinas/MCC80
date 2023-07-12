using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

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
        MenuUtama();
        Console.Write("Masukkan Pilihan : ");
        string choice = Console.ReadLine();
        Console.Clear();

        switch (choice)
        {
            case "1":
                Console.WriteLine("=== Menu Tabel Region ===");
                MenuReg();
                string input1 = Console.ReadLine();
                break;
            case "2":
                Console.WriteLine("=== Menu Tabel Country ===");
                MenuCoun();
                string input2 = Console.ReadLine();
                //Country.InsertCountry(input2, _connectionString);
                break;
            case "3":
                Console.WriteLine("=== Menu Tabel Location ===");
                //MenuCrud();
                //Location
                break;
            case "4":
                Console.WriteLine("=== Menu Tabel Departement ===");
                //MenuCrud();
                break;
            case "5":
                Console.WriteLine("=== Menu Tabel Employee ===");
                //MenuCrud();
                break;
            case "6":
                Console.WriteLine("=== Menu Tabel Job ===");
                MenuJob();
                Console.Write("Input : ");
                string input6 = Console.ReadLine();
                break;
            case "7":
                Console.WriteLine("=== Menu Tabel History ===");
                //MenuCrud();
                break;
            case "8":
                Console.WriteLine("Keluar dari program");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

    }

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


    //===========Region========================
    //GET ALL REGION
    public static void MenuReg()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By Id");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.WriteLine();
            Console.Write("Masukkan Pilihan : ");
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {
                case 1:
                    Console.Clear();
                    TambahRegion();
                    MenuReg();
                    break;
                case 2:
                    Console.Clear();
                    UbahRegion();
                    MenuReg();
                    break;
                case 3:
                    Console.Clear();
                    HapusRegion();
                    MenuReg();
                    break;
                case 4:
                    Console.Clear();
                    CariIdReg();
                    MenuReg();
                    break;
                case 5:
                    GetRegions();
                    MenuReg();
                    break;
                case 6:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Tidak ada pilihan");
                    MenuReg();
                    break;
            }
        }
    }

    public static void GetRegions()
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
    public static void InsertRegion(string name)
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
                Console.WriteLine("Insert succes");
            }
            else
            {
                Console.WriteLine("Insert failed");
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

    public static void TambahRegion()
    {
        Console.Write("Tambah Nama Region: ");
        string inputName = Console.ReadLine();
        InsertRegion(inputName);
    }


    // UPDATE REGION
    public static void UpdateRegion(int id, string name)
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

    public static void UbahRegion()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        int inputId = Int32.Parse(Console.ReadLine());
        Console.Write("Ubah Region Name: ");
        string inputName = Console.ReadLine();
        UpdateRegion(inputId, inputName);
    }

    // DELETE REGION
    public static void DeleteRegions(int id)
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

    public static void HapusRegion()
    {
        Console.Write("Hapus Region Id: ");
        int inputId = Int32.Parse(Console.ReadLine());
        DeleteRegions(inputId);
    }

    // GET BY ID REGION
    public static void GetByIdReg(int id)
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

    public static void CariIdReg()
    {
        Console.Write("Cari region Id: ");
        int inputId = Int32.Parse(Console.ReadLine());
        GetByIdReg(inputId);
    }


    //===========Countries========================
    public static void MenuCoun()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By Id");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.WriteLine();
            Console.Write("Masukkan Pilihan : ");
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {
                case 1:
                    Console.Clear();
                    TambahCoun();
                    MenuCoun();
                    break;
                case 2:
                    Console.Clear();
                    UbahCoun();
                    MenuCoun();
                    break;
                case 3:
                    Console.Clear();
                    HapusCount();
                    MenuCoun();
                    break;
                case 4:
                    Console.Clear();
                    CariIdCoun();
                    MenuCoun();
                    break;
                case 5:
                    GetCoun();
                    MenuCoun();
                    break;
                case 6:
                    MenuUtama();
                    break;
                default:
                    Console.WriteLine("Tidak ada pilihan");
                    MenuCoun();
                    break;
            }
        }
    }
    // Get all countries
    public static void GetCoun()
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM countries";

        try
        {
            _connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine("Region ID: " + reader.GetInt32(2));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No country found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Error! "+ ex.Message);
        }
    }

    // Insert Country
    public static void InsertCountry(string id, string name, int region_id)
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

    public static void TambahCoun()
    {
        Console.Write("Input ID Country: ");
        string inputID = Console.ReadLine();
        Console.Write("Input Country: ");
        string inputName = Console.ReadLine();
        Console.Write("Input ID Region: ");
        int inputRegId = Int32.Parse(Console.ReadLine());
        InsertCountry(inputID, inputName, inputRegId);
    }

    public static void UpdateCount(string id, string name, int region_id)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Update countries set name = @name, region_id = @region_id where id = @id";

        //Set paramaeter value
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@name", name);
        sqlCommand.Parameters.AddWithValue("@region_id", region_id);

        try
        {
            _connection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Console.WriteLine("Countries updated succesfully.");
            }
            else
            {
                Console.WriteLine("No countries found or no change made.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static void UbahCoun()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        string inputId = Console.ReadLine();
        Console.Write("Ubah Country Name: ");
        string inputCountri = Console.ReadLine();
        Console.Write("Ubah Region ID: ");
        int inputRegID = Int32.Parse(Console.ReadLine());
        UpdateCount(inputId, inputCountri, inputRegID);
    }

    public static void DeleteCoun(string id)
    {
        var _connection = new SqlConnection(_connectionString);
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Delete from countries where id = @Id";

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

    public static void HapusCount()
    {
        Console.Write("Hapus Country Id: ");
        string inputId = Console.ReadLine();
        DeleteCoun(inputId);
    }

    // GET BY ID REGION
    public static void GetByIdCoun(string id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM countries WHERE id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID: " + reader.GetString(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                    Console.WriteLine("ID Region: " + reader.GetInt32(2));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No countries found.");
            }

            reader.Close();
            //_connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!" + ex.Message);
        }
    }

    public static void CariIdCoun()
    {
        Console.Write("Cari Id Country: ");
        string inputId = Console.ReadLine();
        GetByIdCoun(inputId);
    }


    //============Location======================
    // Get all locations
    public static void GetLocation()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM locations";

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
                Console.WriteLine("No location found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

    // Get all departement
    public static void GetDepartemen()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM departements";

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
                Console.WriteLine("No departement found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

    // Get all employee
    public static void GetEmployee()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM employees";

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
                Console.WriteLine("No employees found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

    //===========Jobs========================
    public static void MenuJob()
    {
        Console.WriteLine("1. Create");
        Console.WriteLine("2. Update");
        Console.WriteLine("3. Delete");
        Console.WriteLine("4. Get By Id");
        Console.WriteLine("5. Get All");
        Console.WriteLine("6. Back");
        Console.WriteLine();
        Console.Write("Masukkan Pilihan : ");
        int pilihMenu = Int32.Parse(Console.ReadLine());

        switch (pilihMenu)
        {
            case 1:
                Console.Clear();
                TambahJob();
                MenuJob();
                break;
            case 2:
                Console.Clear();
                UbahJob();
                MenuJob();
                break;
            case 3:
                Console.Clear();
                HapusJob();
                MenuJob();
                break;
            case 4:
                Console.Clear();
                CariIdJob();
                MenuJob();
                break;
            case 5:
                GetJob();
                MenuJob();
                break;
            case 6:
                Console.Clear();
                break; 
            default:
                Console.WriteLine("Tidak ada pilihan");
                MenuJob();
                break;
        }
    }
    // Get all jobs
    public static void GetJob()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM jobs";

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetString(0));
                    Console.WriteLine("Title: " + reader.GetString(1));
                    Console.WriteLine("Minimal Salary: " + reader.GetInt32(2));
                    Console.WriteLine("Maximal Salary: " + reader.GetInt32(3));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No jobs found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

    // INSERT JOB
    public static void InsertJob(string id, string title, int min_salary, int max_salary)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO jobs(id, title, min_salary, max_salary) " +
            "VALUES (@id, @title, @min_salary, @max_salary)";

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

            SqlParameter pTitle = new SqlParameter();
            pTitle.ParameterName = "@title";
            pTitle.SqlDbType = SqlDbType.VarChar;
            pTitle.Value = title;
            sqlCommand.Parameters.Add(pTitle);

            SqlParameter pMinSalary = new SqlParameter();
            pMinSalary.ParameterName = "@min_salary";
            pMinSalary.SqlDbType = SqlDbType.Int;
            pMinSalary.Value = min_salary;
            sqlCommand.Parameters.Add(pMinSalary);

            SqlParameter pMaxSalary = new SqlParameter();
            pMaxSalary.ParameterName = "@max_salary";
            pMaxSalary.SqlDbType = SqlDbType.VarChar;
            pMaxSalary.Value = max_salary;
            sqlCommand.Parameters.Add(pMaxSalary);

            int result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Insert succes");
            }
            else
            {
                Console.WriteLine("Insert failed");
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

    // UPDATE JOB
    public static void UpdateJob(string id, string title, int min_salary, int max_salary)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "update jobs set title = @title, min_salary = @min_salary, max_salary = @max_salary" +
            " WHERE id = @idJob";

        //Set paramaeter value
        sqlCommand.Parameters.AddWithValue("@idJob", id);
        sqlCommand.Parameters.AddWithValue("@title", title);
        sqlCommand.Parameters.AddWithValue("@min_salary", min_salary);
        sqlCommand.Parameters.AddWithValue("@max_salary", max_salary);
        try
        {
            _connection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Console.WriteLine("Job updated succesfully.");
            }
            else
            {
                Console.WriteLine("No job found or no change made.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

    }

    public static void UbahJob()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        string id = Console.ReadLine();
        Console.Write("Ubah Title: ");
        string title = Console.ReadLine();
        Console.Write("Ubah Gaji Minimal : ");
        int min_salary = Int32.Parse( Console.ReadLine());
        Console.Write("Ubah Gaji Maksimal : ");
        int max_salary = Int32.Parse(Console.ReadLine());
        UpdateJob(id, title, min_salary, max_salary);

    }

    // DELETE Job
    public static void DeleteJob(string id)
    {
        var _connection = new SqlConnection(_connectionString);
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Delete from jobs where id = (@Id)";

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

    public static void HapusJob()
    {
        Console.Write("Hapus Job dengan ID: ");
        string inputId = Console.ReadLine();
        DeleteJob(inputId);
    }

    public static void GetByIdJob(string id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM jobs WHERE id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID: " + reader.GetString(0));
                    Console.WriteLine("Title: " + reader.GetString(1));
                    Console.WriteLine("Min Salary: " + reader.GetInt32(2));
                    Console.WriteLine("Max Salary: " + reader.GetInt32(3));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No job found.");
            }

            reader.Close();
            //_connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!" + ex.Message);
        }
    }

    public static void CariIdJob()
    {
        Console.Write("Cari Job Id: ");
        string inputId = Console.ReadLine();
        GetByIdJob(inputId);
    }

    public static void TambahJob()
    {
        Console.Write("Tambah ID: ");
        string inId = Console.ReadLine();
        Console.Write("Tambah Title: ");
        string inTitle = Console.ReadLine();
        Console.Write("Tambah Min Salary: ");
        int inMinSal = Int32.Parse(Console.ReadLine());
        Console.Write("Tambah Max Salary: ");
        int inMaxSal = Int32.Parse(Console.ReadLine());
        InsertJob(inId, inTitle, inMinSal, inMaxSal);
    }

    // GET BY ID Job
    public static void GetByIdRegion(int id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT name FROM jobs WHERE id = @Id";
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
                Console.WriteLine("No job found.");
            }

            reader.Close();
            //_connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!" + ex.Message);
        }
    }



    //==========================Histories==============================================
    // Get all histories
    public static void GetHistories()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM histories";

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
                Console.WriteLine("No histories found.");
            }

            reader.Close();
            _connection.Close();
        }
        catch
        {
            Console.WriteLine("Error connecting to database.");
        }
    }

}





