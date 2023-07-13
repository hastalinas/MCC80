using MVC_Achitecture.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace MVC_Achitecture.Models;

public class Country
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }

    public List<Country> GetAll()
    {
        var connection = Connection.Get();
        var countries = new List<Country>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "select * from countries";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();    
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Country country = new Country();
                    country.Id = reader.GetString(0);
                    country.Name = reader.GetString(1);
                    country.RegionId = reader.GetInt32(2);

                    countries.Add(country);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return countries;
        }
        catch (Exception ex)
        {
            return new List<Country>();
        }
    }

    public int Insert(Country country)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO countries (id, name, region_id) VALUES (@id, @name, @region_id)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = country.Id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = country.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegId = new SqlParameter();
            pRegId.ParameterName = "@region_id";
            pRegId.SqlDbType = SqlDbType.Int;
            pRegId.Value = country.RegionId;
            sqlCommand.Parameters.Add(pRegId);

            int result = sqlCommand.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();

            return result;
        }
        catch
        {
            transaction.Rollback();
            return -1;
            //Console.WriteLine("Error connecting to database");
        }

    }

    public int Update(Country country)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Update countries set name = @name, region_id = @region_id where id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = country.Id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = country.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegId = new SqlParameter();
            pRegId.ParameterName = "@region_id";
            pRegId.SqlDbType = SqlDbType.Int;
            pRegId.Value = country.RegionId;
            sqlCommand.Parameters.Add(pRegId);

            int result = sqlCommand.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();

            return result;
        }
        catch 
        {
            transaction.Rollback();
            return -1;
        }
    }

    public int Delete(Country country)
    {
        var connection = Connection.Get();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Delete from countries where id = @Id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = country.Id;
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
        catch
        {
            transaction.Rollback();
            return -1;
            //Console.WriteLine("Error! " + ex.Message);
        }
    }

    public Country GetById(string id)
    {
        var country = new Country();
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM countries WHERE id = @Id";
        sqlCommand.Parameters.AddWithValue("@Id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                country.Id = reader.GetString(0);
                country.Name = reader.GetString(1);
            }

            reader.Close();
            connection.Close();

            return new Country();
        }
        catch
        {
            return new Country();
        }
    }

}
