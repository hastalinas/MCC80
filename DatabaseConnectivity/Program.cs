using System.Collections.Generic;
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
                break;
            case "3":
                Console.WriteLine("=== Menu Tabel Location ===");
                MenuLoc();
                Console.Write("Input : ");
                string input3 = Console.ReadLine();
                break;
            case "4":
                Console.WriteLine("=== Menu Tabel Departement ===");
                MenuDep();
                Console.Write("Input : ");
                string input4 = Console.ReadLine();
                break;
            case "5":
                Console.WriteLine("=== Menu Tabel Employee ===");
                //MenuEmp();
                //Console.Write("Input : ");
                //string input5 = Console.ReadLine();
                break;
            case "6":
                Console.WriteLine("=== Menu Tabel Job ===");
                MenuJob();
                Console.Write("Input : ");
                string input6 = Console.ReadLine();
                break;
            case "7":
                Console.WriteLine("=== Menu Tabel History ===");
                MenuHis();
                Console.Write("Input : ");
                string input7 = Console.ReadLine();
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
    public static void MenuLoc()
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
                    TambahLoc();
                    MenuLoc();
                    break;
                case 2:
                    Console.Clear();
                    UbahLoc();
                    MenuLoc();
                    break;
                case 3:
                    Console.Clear();
                    HapusLoc();
                    MenuLoc();
                    break;
                case 4:
                    Console.Clear();
                    CariIdLoc();
                    MenuLoc();
                    break;
                case 5:
                    GetLocation();
                    MenuLoc();
                    break;
                case 6:
                    MenuUtama();
                    break;
                default:
                    Console.WriteLine("Tidak ada pilihan");
                    MenuLoc();
                    break;
            }
        }
    }

    public static void GetByIdLoc(int id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT l.id,street_address,postal_code,city,state_province, c.name " +
            "FROM locations l join countries c on l.country_id = c.id WHERE l.id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id          : " + reader.GetInt32(0));
                    Console.WriteLine("Street      : " + reader.GetString(1));
                    Console.WriteLine("Postal Code : " + reader.GetString(2));
                    Console.WriteLine("City        : " + reader.GetString(3));
                    Console.WriteLine("State       : " + reader.GetString(4));
                    Console.WriteLine("Country     : " + reader.GetString(5));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No locations found.");
            }

            reader.Close();
            //_connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!" + ex.Message);
        }
    }

    public static void CariIdLoc()
    {
        Console.Write("Cari Id Location: ");
        int inputId = Int32.Parse(Console.ReadLine());
        GetByIdLoc(inputId);
    }
    public static void DeleteLoc(int id)
    {
        var _connection = new SqlConnection(_connectionString);
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Delete from locations where id = @Id";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.SqlDbType = SqlDbType.Int;
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

    public static void HapusLoc()
    {
        Console.Write("Hapus Location Id: ");
        int inputId = Int32.Parse(Console.ReadLine());
        DeleteLoc(inputId);
    }
    public static void UpdateLoc(int id, string street_address, string postal_code, string city, string state_province,
        string country_id)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Update locations set street_address = @street_address, postal_code = @postal_code, city = @city," +
            "state_province = @state_province, country_id=@country_id where id = @id";

        //Set paramaeter value
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@street_address", street_address);
        sqlCommand.Parameters.AddWithValue("@postal_code", postal_code);
        sqlCommand.Parameters.AddWithValue("@city", city);
        sqlCommand.Parameters.AddWithValue("@state_province", state_province);
        sqlCommand.Parameters.AddWithValue("@country_id", country_id);

        try
        {
            _connection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Console.WriteLine("Locations updated succesfully.");
            }
            else
            {
                Console.WriteLine("No Locations found or no change made.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static void UbahLoc()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        int inputId = Int32.Parse(Console.ReadLine());

        Console.Write("Update Street Address : ");
        string instreet_address = Console.ReadLine();

        Console.Write("Update Postal Code : ");
        string inpostal_code = Console.ReadLine();

        Console.Write("Update City : ");
        string incity = Console.ReadLine();

        Console.Write("Update State Province: ");
        string instate_province = Console.ReadLine();

        Console.Write("Update Contry Id: ");
        string incountry_id = Console.ReadLine();

        UpdateLoc(inputId, instreet_address, inpostal_code,incity, instate_province,incountry_id);
    }

    public static void InsertLoc(int id, string street_address, string postal_code, string city, string state_province,
        string country_id)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO locations (id, street_address, postal_code, city, state_province, country_id) " +
            "VALUES (@id, @street_address, @postal_code, @city, @state_province, @country_id)";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pSA = new SqlParameter();
            pSA.ParameterName = "@street_address";
            pSA.SqlDbType = SqlDbType.VarChar;
            pSA.Value = street_address;
            sqlCommand.Parameters.Add(pSA);

            SqlParameter ppostal_code = new SqlParameter();
            ppostal_code.ParameterName = "@postal_code";
            ppostal_code.SqlDbType = SqlDbType.VarChar;
            ppostal_code.Value = postal_code;
            sqlCommand.Parameters.Add(ppostal_code);

            SqlParameter pcity = new SqlParameter();
            pcity.ParameterName = "@city";
            pcity.SqlDbType = SqlDbType.VarChar;
            pcity.Value =city;
            sqlCommand.Parameters.Add(pcity);

            SqlParameter pstate_province = new SqlParameter();
            pstate_province.ParameterName = "@state_province";
            pstate_province.SqlDbType = SqlDbType.VarChar;
            pstate_province.Value = postal_code;
            sqlCommand.Parameters.Add(pstate_province);

            SqlParameter pcountry_id = new SqlParameter();
            pcountry_id.ParameterName = "@country_id";
            pcountry_id.SqlDbType = SqlDbType.Char;
            pcountry_id.Value = country_id;
            sqlCommand.Parameters.Add(pcountry_id);

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
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine("Error!" + ex.Message);
        }

    }

    public static void TambahLoc()
    {
        Console.Write("Input ID Location: ");
        int inputID = Int32.Parse(Console.ReadLine());
        Console.Write("Input Address: ");
        string inputAddress = Console.ReadLine();
        Console.Write("Input Postal Code: ");
        string inputPos = Console.ReadLine();
        Console.Write("Input City: ");
        string inputCity = Console.ReadLine();
        Console.Write("Input State Province: ");
        string inputProv = Console.ReadLine();
        Console.Write("Input Country ID: ");
        string inputCoun = Console.ReadLine();
        InsertLoc(inputID, inputAddress, inputPos, inputCity, inputProv, inputCoun);
    }
    // Get all locations
    public static void GetLocation()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT l.id,street_address,postal_code,city,state_province, c.name " +
            "FROM locations l join countries c on l.country_id = c.id";

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id          : " + reader.GetInt32(0));
                    Console.WriteLine("Street      : " + reader.GetString(1));
                    Console.WriteLine("Postal Code : " + reader.GetString(2));
                    Console.WriteLine("City        : " + reader.GetString(3));
                    Console.WriteLine("State       : " + reader.GetString(4));
                    Console.WriteLine("Country     : " + reader.GetString(5));
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



    //============Departement======================
    // Get all departement
    public static void MenuDep()
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
                    TambahDep();
                    MenuDep();
                    break;
                case 2:
                    Console.Clear();
                    UbahDep();
                    MenuDep();
                    break;
                case 3:
                    Console.Clear();
                    HapusDep();
                    MenuDep();
                    break;
                case 4:
                    Console.Clear();
                    CariIdDep();
                    MenuDep();
                    break;
                case 5:
                    GetDepartemen();
                    MenuDep();
                    break;
                case 6:
                    MenuUtama();
                    break;
                default:
                    Console.WriteLine("Tidak ada pilihan");
                    MenuDep();
                    break;
            }
        }
    }

    // Get all dep
    public static void GetDepartemen()
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT d.id, d.name, l.city, d.manager_id " +
            "FROM departements d JOIN locations l ON d.location_id = l.Id";

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Departement Name: " + reader.GetString(1));
                    Console.WriteLine("Location Name : " + reader.GetString(2));
                    Console.WriteLine("Manager ID: " + reader.GetInt32(3));
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

    // insert departemen
    public static void InsertDep(int id, string name, int location_id, int manager_id)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "INSERT INTO departements (id, name, location_id, manager_id) " +
            "VALUES (@id, @name, @location_id, @manager_id)";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pLocId = new SqlParameter();
            pLocId.ParameterName = "@location_id";
            pLocId.SqlDbType = SqlDbType.Int;
            pLocId.Value = location_id;
            sqlCommand.Parameters.Add(pLocId);

            SqlParameter pManId = new SqlParameter();
            pManId.ParameterName = "@manager_id";
            pManId.SqlDbType = SqlDbType.Int;
            pManId.Value = manager_id;
            sqlCommand.Parameters.Add(pManId);

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
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine("Error!" + ex.Message);
        }

    }
    public static void TambahDep()
    {
        Console.Write("Input ID Departemen: ");
        int inputID = Int32.Parse(Console.ReadLine());
        Console.Write("Input Departement Name: ");
        string inputDepName = Console.ReadLine();
        Console.Write("Input ID Location: ");
        int inpuLocId = Int32.Parse(Console.ReadLine());
        Console.Write("Input ID Manager: ");
        int inputManID = Int32.Parse(Console.ReadLine());
        InsertDep(inputID, inputDepName, inpuLocId, inputManID);
    }

    public static void GetByIdDep(string id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT d.id, d.name, l.city, d.manager_id FROM departements d JOIN locations l ON d.location_id = l.id WHERE d.id=@id";
        sqlCommand.Parameters.AddWithValue("@id", id);

        try
        {
            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Departement Name: " + reader.GetString(1));
                    Console.WriteLine("Location Name : " + reader.GetString(2));
                    Console.WriteLine("Manager ID: " + reader.GetInt32(3));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No departements found.");
            }

            reader.Close();
            //_connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!" + ex.Message);
        }
    }

    public static void CariIdDep()
    {
        Console.Write("Cari Id Departement: ");
        string inputId = Console.ReadLine();
        GetByIdDep(inputId);
    }
    public static void DeleteDep(string id)
    {
        var _connection = new SqlConnection(_connectionString);
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Delete from departements where id = @Id";

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

    public static void HapusDep()
    {
        Console.Write("Hapus Departemen Id: ");
        string inputId = Console.ReadLine();
        DeleteDep(inputId);
    }

    public static void UpdateDep(string id, string name, int location_id, int manager_id)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Update departements set name = @name, location_id = @location_id, manager_id = @manager_id where id = @id";

        //Set paramaeter value
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Parameters.AddWithValue("@name", name);
        sqlCommand.Parameters.AddWithValue("@location_id", location_id);
        sqlCommand.Parameters.AddWithValue("@manager_id", manager_id);

        try
        {
            _connection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Console.WriteLine("Departement updated succesfully.");
            }
            else
            {
                Console.WriteLine("No Departement found or no change made.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    public static void UbahDep()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        string inputId = Console.ReadLine();
        Console.Write("Ubah Departement Name: ");
        string inputDep = Console.ReadLine();
        Console.Write("Ubah Location ID: ");
        int inputLocID = Int32.Parse(Console.ReadLine());
        Console.Write("Ubah Manager ID: ");
        int inputManID = Int32.Parse(Console.ReadLine());
        UpdateDep(inputId, inputDep, inputLocID, inputManID);
    }


    //============employee==========
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
/*    public static void GetByIdRegion(int id)
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
    }*/



    //==========================Histories==============================================
    public static void MenuHis()
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
                    TambahHis();
                    MenuHis();
                    break;
                case 2:
                    Console.Clear();
                    UbahHis();
                    MenuLoc();
                    break;
                case 3:
                    Console.Clear();
                    HapusHis();
                    MenuLoc();
                    break;
                case 4:
                    Console.Clear();
                    CariIdHis();
                    MenuLoc();
                    break;
                case 5:
                    GetHistories();
                    MenuHis();
                    break;
                case 6:
                    MenuUtama();
                    break;
                default:
                    Console.WriteLine("Tidak ada pilihan");
                    MenuLoc();
                    break;
            }
        }
    }



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
                    Console.WriteLine("Start Date     : " + reader.GetDateTime(0));
                    Console.WriteLine("Employee ID    : " + reader.GetInt32(1));
                    Console.WriteLine("End Date       : " + reader.GetDateTime(2));
                    Console.WriteLine("Departement ID : " + reader.GetInt32(3));
                    Console.WriteLine("Job ID         : " + reader.GetString(4));
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

    public static void InsertHist(DateTime start_date, int employee_id, DateTime end_date, 
        int departement_id, string job_id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlConnection connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO histories (start_date, employee_id, end_date, departement_id, job_id) " +
                           "VALUES (@StartDate, @EmployeeID, @EndDate, @DepartmentID, @JobID)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StartDate", start_date);
                command.Parameters.AddWithValue("@EmployeeID", employee_id);
                command.Parameters.AddWithValue("@EndDate", end_date);
                command.Parameters.AddWithValue("@DepartmentID", departement_id);
                command.Parameters.AddWithValue("@JobID", job_id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("History record inserted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert history record.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            
        }
    }
    public static void TambahHis()
    {
        Console.Write("Input Start Date (YYYY-MM-DD): ");
        string inStart = Console.ReadLine();
        DateTime startDate;
        if (DateTime.TryParse(inStart, out startDate))
        {
            // Valid DateTime value
            Console.WriteLine("Start Date: " + startDate.ToString("yyyy-MM-dd"));
        }
        else
        {
            // Invalid DateTime value
            Console.WriteLine("Invalid Start Date format. Please enter a valid date (YYYY-MM-DD).");
            return; // Exit the method if the date is invalid
        }

        Console.Write("Input Employee ID: ");
        int inEmpID = Int32.Parse(Console.ReadLine());

        Console.Write("Input End Date (YYYY-MM-DD): ");
        string inEnd = Console.ReadLine();
        DateTime endDate;
        if (DateTime.TryParse(inEnd, out endDate))
        {
            // Valid DateTime value
            Console.WriteLine("End Date: " + endDate.ToString("yyyy-MM-dd"));
        }
        else
        {
            // Invalid DateTime value
            Console.WriteLine("Invalid End Date format. Please enter a valid date (YYYY-MM-DD).");
            return; // Exit the method if the date is invalid
        }

        Console.Write("Input Departemen ID: ");
        int inputDepID = Int32.Parse(Console.ReadLine());

        Console.Write("Input Job ID: ");
        string inJobID = Console.ReadLine();
        InsertHist(startDate, inEmpID, endDate, inputDepID, inJobID);
    }

    public static void UpdateHis(DateTime start_date, int employee_id, DateTime end_date,
        int departement_id, string job_id)
    {
        var _connection = new SqlConnection(_connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "update histories set start_date = @start_date, job_id = @job_id, " +
            "departement_id = @departement_id" +
            " WHERE employee_id = @employee_id";

        //Set paramaeter value
        sqlCommand.Parameters.AddWithValue("@start_date", start_date);
        sqlCommand.Parameters.AddWithValue("@employee_id", employee_id);
        sqlCommand.Parameters.AddWithValue("@end_date", end_date);
        sqlCommand.Parameters.AddWithValue("@departement_id", departement_id);
        sqlCommand.Parameters.AddWithValue("@job_id", job_id);
        try
        {
            _connection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Console.WriteLine("Histories updated succesfully.");
            }
            else
            {
                Console.WriteLine("No histories found or no change made.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

    }

    public static void UbahHis()
    {
        Console.Write("Input Employee ID: ");
        int inEmpID = int.Parse(Console.ReadLine());

        Console.Write("Input Start Date (YYYY-MM-DD): ");
        string inStart = Console.ReadLine();
        DateTime startDate;
        if (DateTime.TryParse(inStart, out startDate))
        {
            // Valid DateTime value
            Console.WriteLine("Start Date: " + startDate.ToString("yyyy-MM-dd"));
        }
        else
        {
            // Invalid DateTime value
            Console.WriteLine("Invalid Start Date format. Please enter a valid date (YYYY-MM-DD).");
            return;
        }

        Console.Write("Input End Date (YYYY-MM-DD): ");
        string inEnd = Console.ReadLine();
        DateTime endDate;
        if (DateTime.TryParse(inEnd, out endDate))
        {
            // Valid DateTime value
            Console.WriteLine("End Date: " + endDate.ToString("yyyy-MM-dd"));
        }
        else
        {
            // Invalid DateTime value
            Console.WriteLine("Invalid End Date format. Please enter a valid date (YYYY-MM-DD).");
            return;
        }

        Console.Write("Input Department ID: ");
        int inputDepID = int.Parse(Console.ReadLine());

        Console.Write("Input Job ID: ");
        string inJobID = Console.ReadLine();

        UpdateHis(startDate, inEmpID, endDate, inputDepID, inJobID);
    }

    public static void DeleteHis(int id)
    {
        var _connection = new SqlConnection(_connectionString);
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "Delete from histories where employee_id = (@Id)";

        _connection.Open();
        SqlTransaction transaction = _connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.SqlDbType = SqlDbType.Int;
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

    public static void HapusHis()
    {
        Console.Write("Hapus Histori dengan ID Employee: ");
        int inputId = Int32.Parse(Console.ReadLine());
        DeleteHis(inputId);
    }

    public static void GetByIdHis(int id)
    {
        var _connection = new SqlConnection(_connectionString);

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = _connection;
        sqlCommand.CommandText = "SELECT * FROM histories WHERE employee_id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {

            _connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Start Date : " + reader.GetDateTime(0));
                    Console.WriteLine("Employee ID: " + reader.GetInt32(1));
                    Console.WriteLine("End Date   : " + reader.GetDateTime(2));
                    Console.WriteLine("Job        : " + reader.GetInt32(3));
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

    public static void CariIdHis()
    {
        Console.Write("Cari Histories Berdasarkan Employee Id: ");
        int inputId =Int32.Parse(Console.ReadLine());
        GetByIdHis(inputId);
    }
}





