using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Data.Common;

namespace MVC_Achitecture.Models;

public class Location
{
    public int Id { get; set; }
    public string StreetAdd { get; set; }
    public string PostalCod { get; set;}
    public string City { get; set; }    
    public string StateProp { get; set; }
    public string IdCountry { get; set; }

    public List<Location> GetAll()
    {
        var connection = Connection.Get();
        var locations = new List<Location>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT l.id,street_address,postal_code,city,state_province, c.name " +
            "FROM locations l join countries c on l.country_id = c.id";

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                        /* Console.WriteLine("Id          : " + reader.GetInt32(0));
                           Console.WriteLine("Street      : " + reader.GetString(1));
                           Console.WriteLine("Postal Code : " + reader.GetString(2));
                           Console.WriteLine("City        : " + reader.GetString(3));
                           Console.WriteLine("State       : " + reader.GetString(4));
                           Console.WriteLine("Country     : " + reader.GetString(5));
                           Console.WriteLine();*/
                    Location location = new Location();
                    location.Id = reader.GetInt32(0);
                    location.StreetAdd = reader.GetString(1);
                    location.PostalCod = reader.GetString(2);
                    location.City = reader.GetString(3);
                    location.StateProp = reader.GetString(4);
                    location.IdCountry = reader.GetString(5);
                    locations.Add(location);
                }
            }
            else
            {
                //Console.WriteLine("No location found.");
                reader.Close();
                connection.Close();
            }
            return locations;
        }
        catch
        {
            return new List<Location>();
            //Console.WriteLine("Error connecting to database.");
        }
    }

    public int Insert(Location location)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO locations (id, street_address, postal_code, city, state_province, country_id) " +
            "VALUES (@id, @street_address, @postal_code, @city, @state_province, @country_id)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.Int;
            pId.Value = location.Id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pSA = new SqlParameter();
            pSA.ParameterName = "@street_address";
            pSA.SqlDbType = SqlDbType.VarChar;
            pSA.Value = location.StreetAdd;
            sqlCommand.Parameters.Add(pSA);

            SqlParameter ppostal_code = new SqlParameter();
            ppostal_code.ParameterName = "@postal_code";
            ppostal_code.SqlDbType = SqlDbType.VarChar;
            ppostal_code.Value = location.PostalCod;
            sqlCommand.Parameters.Add(ppostal_code);

            SqlParameter pcity = new SqlParameter();
            pcity.ParameterName = "@city";
            pcity.SqlDbType = SqlDbType.VarChar;
            pcity.Value = location.City;
            sqlCommand.Parameters.Add(pcity);

            SqlParameter pstate_province = new SqlParameter();
            pstate_province.ParameterName = "@state_province";
            pstate_province.SqlDbType = SqlDbType.VarChar;
            pstate_province.Value = location.StateProp;
            sqlCommand.Parameters.Add(pstate_province);

            SqlParameter pcountry_id = new SqlParameter();
            pcountry_id.ParameterName = "@country_id";
            pcountry_id.SqlDbType = SqlDbType.Char;
            pcountry_id.Value = location.IdCountry;
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
            //connection.Close();
            return -1;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine("Error!" + ex.Message);
            return -1;
        }
    }

    public int Update(Location location)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Update locations set street_address = @street_address, postal_code = @postal_code, city = @city," +
            "state_province = @state_province, country_id=@country_id where id = @id";
       
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            //Set paramaeter value
            sqlCommand.Parameters.AddWithValue("@id", Id);
            sqlCommand.Parameters.AddWithValue("@street_address", StreetAdd);
            sqlCommand.Parameters.AddWithValue("@postal_code", PostalCod);
            sqlCommand.Parameters.AddWithValue("@city", City);
            sqlCommand.Parameters.AddWithValue("@state_province", StateProp);
            sqlCommand.Parameters.AddWithValue("@country_id", IdCountry);

            int result = sqlCommand.ExecuteNonQuery();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return 1;
        }
    }

    public int Delete(int id)
    {
        var connection = Connection.Get();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Delete from locations where id = @Id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
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
            connection.Close();
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine("Error! " + ex.Message);
            return -1;
        }
    }

    public Location GetById(int id)
    {
        var location = new Location();

        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT l.id,street_address,postal_code,city,state_province, c.name " +
            "FROM locations l join countries c on l.country_id = c.id WHERE l.id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {
            connection.Open();
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
            connection.Close();
            return new Location();
        }
        catch 
        {
            return new Location();
            //Console.WriteLine("Error!" + ex.Message);
        }
    }
}
