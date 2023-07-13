using MVC_Achitecture.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Models;

public class History
{
    public DateTime StartDate { get; set; }

    public int EmployeeId { get; set; }

    public DateTime EndDate { get; set; }

    public int DepartmentId { get; set; }

    public string JobId { get; set; }
    public List<History> GetAll()
    {
        var connection = Connection.Get();
        var histories = new List<History>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM histories";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    History history = new History();
                    history.StartDate = reader.GetDateTime(0);
                    history.EmployeeId = reader.GetInt32(1);
                    history.EndDate = reader.GetDateTime(2);
                    history.DepartmentId = reader.GetInt32(3);
                    history.JobId = reader.GetString(4);


                    histories.Add(history);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }

            return histories;
        }
        catch
        {
            return new List<History>();
        }
    }

    public int Insert(History history)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO histories (start_date, employee_id, end_date, departement_id, job_id) VALUES (@StartDate, @EmployeeID, @EndDate, @DepartmentID, @JobID)";

        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pStartDate = new SqlParameter();
            pStartDate.ParameterName = "@StartDate";
            pStartDate.SqlDbType = SqlDbType.DateTime;
            pStartDate.Value = history.StartDate;
            sqlCommand.Parameters.Add(pStartDate);

            SqlParameter pEmployeeID = new SqlParameter();
            pEmployeeID.ParameterName = "@EmployeeID";
            pEmployeeID.SqlDbType = SqlDbType.Int;
            pEmployeeID.Value = history.EmployeeId;
            sqlCommand.Parameters.Add(pEmployeeID);

            SqlParameter pEndDate = new SqlParameter();
            pEndDate.ParameterName = "@EndDate";
            pEndDate.SqlDbType = SqlDbType.DateTime;
            pEndDate.Value = history.EndDate;
            sqlCommand.Parameters.Add(pEndDate);

            SqlParameter pDepartmentID = new SqlParameter();
            pDepartmentID.ParameterName = "@DepartmentID";
            pDepartmentID.SqlDbType = SqlDbType.Int;
            pDepartmentID.Value = history.DepartmentId;
            sqlCommand.Parameters.Add(pDepartmentID);

            SqlParameter pJobID = new SqlParameter();
            pJobID.ParameterName = "@JobID";
            pJobID.SqlDbType = SqlDbType.VarChar;
            pJobID.Value = history.JobId;
            sqlCommand.Parameters.Add(pJobID);

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

    public int Update(History history)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "update histories set start_date = @start_date, job_id = @job_id, " +
            "departement_id = @departement_id" +
            " WHERE employee_id = @employee_id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            sqlCommand.Parameters.AddWithValue("@start_date", StartDate);
            sqlCommand.Parameters.AddWithValue("@employee_id", EmployeeId);
            sqlCommand.Parameters.AddWithValue("@end_date", EndDate);
            sqlCommand.Parameters.AddWithValue("@departement_id", DepartmentId);
            sqlCommand.Parameters.AddWithValue("@job_id", JobId);

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
        sqlCommand.CommandText = "Delete from histories where employee_id = (@Id)";

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

    public History CariIdHis(int id)
    {
        var history = new History();

        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM histories WHERE employee_id = @Id)";
        sqlCommand.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                //history.Id = reader.GetInt32(0);
                //history.Name = reader.GetString(1);

                Console.WriteLine("Start Date : " + reader.GetDateTime(0));
                Console.WriteLine("Employee ID: " + reader.GetInt32(1));
                Console.WriteLine("End Date   : " + reader.GetDateTime(2));
                Console.WriteLine("Job        : " + reader.GetInt32(3));
            }

            reader.Close();
            connection.Close();

            return new History();
        }
        catch
        {
            return new History();
        }
    }
}
