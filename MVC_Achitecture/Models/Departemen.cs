using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVC_Achitecture.Models;

public class Departemen
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Location_id { get; set; }
    public int Manager_id { get; set; }

    public List<Departemen> GetAll()
    {
        var connection = Connection.Get();
        var departements = new List<Departemen>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM departements";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Departemen departemen = new Departemen();
                    departemen.Id = reader.GetInt32(0);
                    departemen.Name = reader.GetString(1);
                    departemen.Location_id = reader.GetInt32(2);
                    departemen.Manager_id = reader.GetInt32(3);

                    departements.Add(departemen);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }

            return departements;
        }
        catch
        {
            return new List<Departemen>();
        }
    }

    public int Insert(Departemen departemen)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO departements (id, name, location_id, manager_id) VALUES (@id, @name, @location_id, @manager_id)";

        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.Int;
            pId.Value = departemen.Id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = departemen.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pLocId = new SqlParameter();
            pLocId.ParameterName = "@location_id";
            pLocId.SqlDbType = SqlDbType.Int;
            pLocId.Value = departemen.Location_id;
            sqlCommand.Parameters.Add(pLocId);

            SqlParameter pManId = new SqlParameter();
            pManId.ParameterName = "@manager_id";
            pManId.SqlDbType = SqlDbType.Int;
            pManId.Value = departemen.Manager_id;
            sqlCommand.Parameters.Add(pManId);

            int result = sqlCommand.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();

            return result; // 0 gagal, >= 1 berhasil
        }
        catch
        {
            transaction.Rollback();
            return -1; // Kesalahan terjadi pada database
        }
    }

    public int Update(Departemen departemen)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Update departements set name = @name, location_id = @location_id, manager_id = @manager_id where id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            sqlCommand.Parameters.AddWithValue("@id", departemen.Id);
            sqlCommand.Parameters.AddWithValue("@name", departemen.Name);
            sqlCommand.Parameters.AddWithValue("@location_id", departemen.Location_id);
            sqlCommand.Parameters.AddWithValue("@manager_id", departemen.Manager_id);

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

    public int Delete(int id)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Delete from departements where id = @Id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@Id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

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

    public Departemen CariIdDep(int id)
    {
        var departemen = new Departemen();

        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM departements WHERE id = @departemen_id";
        sqlCommand.Parameters.AddWithValue("@region_id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                departemen.Id = reader.GetInt32(0);
                departemen.Name = reader.GetString(1);
            }

            reader.Close();
            connection.Close();

            return new Departemen();
        }
        catch
        {
            return new Departemen();
        }
    }


}
