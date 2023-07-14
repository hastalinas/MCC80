using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Models;

public class Job
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int MinSal { get; set; }
    public int MaxSal { get; set; }

    public List<Job> GetAll()
    {
        var connection = Connection.Get();
        var jobs = new List<Job>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM jobs";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Job job = new Job();
                    job.Id = reader.GetString(0);
                    job.Title = reader.GetString(1);
                    job.MinSal = reader.GetInt32(2);
                    job.MaxSal = reader.GetInt32(3);

                    jobs.Add(job);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }

            return jobs;
        }
        catch
        {
            return new List<Job>();
        }
    }

    public int Insert(Job job)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO jobs(id, title, min_salary, max_salary) VALUES (@id, @title, @min_salary, @max_salary)";

        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = SqlDbType.VarChar;
            pId.Value = job.Id;
            sqlCommand.Parameters.Add(pId);

            SqlParameter pTitle = new SqlParameter();
            pTitle.ParameterName = "@title";
            pTitle.SqlDbType = SqlDbType.VarChar;
            pTitle.Value = job.Title;
            sqlCommand.Parameters.Add(pTitle);

            SqlParameter pMinSalary = new SqlParameter();
            pMinSalary.ParameterName = "@min_salary";
            pMinSalary.SqlDbType = SqlDbType.Int;
            pMinSalary.Value = job.MinSal;
            sqlCommand.Parameters.Add(pMinSalary);

            SqlParameter pMaxSalary = new SqlParameter();
            pMaxSalary.ParameterName = "@max_salary";
            pMaxSalary.SqlDbType = SqlDbType.VarChar;
            pMaxSalary.Value = job.MaxSal;
            sqlCommand.Parameters.Add(pMaxSalary);

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
    public int Update(Job job)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "update jobs set title = @title, min_salary = @min_salary, max_salary = @max_salary" +
            " WHERE id = @idJob";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            //Set paramaeter value
            sqlCommand.Parameters.AddWithValue("@idJob", job.Id);
            sqlCommand.Parameters.AddWithValue("@title", job.Title);
            sqlCommand.Parameters.AddWithValue("@min_salary", job.MinSal);
            sqlCommand.Parameters.AddWithValue("@max_salary", job.MaxSal);

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
        sqlCommand.CommandText = "Delete from jobs where id = (@Id)";

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

    public Job CariIdJob(string id)
    {
        var job = new Job();

        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM jobs WHERE id = @Id";
        sqlCommand.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                job.Id = reader.GetString(0);
                job.Title = reader.GetString(1);
                job.MinSal = reader.GetInt32(2);
                job.MaxSal = reader.GetInt32(3);
            }

            reader.Close();
            connection.Close();

            return job;
        }
        catch
        {
            return new Job();
        }
    }


}
