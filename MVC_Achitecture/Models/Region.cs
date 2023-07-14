using System.Data.SqlClient;
using System.Data;

namespace MVC_Achitecture.Models;
public class Region
{
    public int Id {get; set;} 
    public string? Name { get; set;}

    public List<Region> GetAll()
    {
        var connection = Connection.Get();
        var regions = new List<Region>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM regions";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Region region = new Region();
                    region.Id = reader.GetInt32(0);
                    region.Name = reader.GetString(1);

                    regions.Add(region);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }

            return regions;
        }
        catch
        {
            return new List<Region>();
        }
    }

    public int Insert(Region region)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO Regions VALUES (@name)";

        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = region.Name;
            sqlCommand.Parameters.Add(pName);

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
    public int Update(Region region) 
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE regions SET name = @name WHERE id = @region_id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = region.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegionId = new SqlParameter();
            pRegionId.ParameterName = "@region_id";
            pRegionId.SqlDbType = System.Data.SqlDbType.Int;
            pRegionId.Value = region.Id;
            sqlCommand.Parameters.Add(pRegionId);

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
        sqlCommand.CommandText = "DELETE FROM regions WHERE id = @region_id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pRegionId = new SqlParameter();
            pRegionId.ParameterName = "@region_id";
            pRegionId.SqlDbType = SqlDbType.Int;
            pRegionId.Value = id;
            sqlCommand.Parameters.Add(pRegionId);

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

    public Region CariIdReg(int id)
    {
        var region = new Region();

        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM regions WHERE id = @region_id";
        sqlCommand.Parameters.AddWithValue("@region_id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                region.Id = reader.GetInt32(0);
                region.Name = reader.GetString(1);
            }

            reader.Close();
            connection.Close();

            return  region;
        }
        catch
        {
            return new Region();
        }
    }


}

