using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Models;

public class Employee
{
    public DateTime StartDate { get; set; }
    public int Id { get; set; }
    public string FN { get; set; }
    public string LN { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Hire { get; set; }
    public int Salary { get; set; }
    public decimal Com { get; set; }
    public int ManID { get; set; }
    public string JobID { get; set; }

    public int DepID { get; set; }
    public List<Employee> GetAll()
    {
        var connection = Connection.Get();
        var employees = new List<Employee>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM employees";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    /*                    employee.Id = reader.GetInt32(0);
                                        employee.FN = reader.GetString(1);
                                        employee.LN = reader.GetString(2);
                                        employee.Email = reader.GetString(3);
                                        employee.Phone = reader.GetString(4);
                                        employee.Hire = reader.GetDateTime(5);
                                        employee.Salary = reader.GetInt32(6);
                                        employee.Com = reader.GetInt32(7);
                                        employee.ManID = reader.GetInt32(8);
                                        employee.JobID = reader.GetString(9);
                                        employee.DepID = reader.GetInt32(10);*/

                    employee.Id = reader.GetInt32(0);
                    employee.FN = reader.GetString(1);
                    employee.LN = (reader.IsDBNull(2) ? "" : reader.GetString(2));
                    employee.Email = reader.GetString(3);
                    employee.Phone = (reader.IsDBNull(4) ? "" : reader.GetString(4));
                    employee.Hire = reader.GetDateTime(5);
                    employee.Salary = reader.GetInt32(6);
                    employee.Com = reader.GetDecimal(7);
                    employee.ManID = (reader.IsDBNull(8) ? 0 : reader.GetInt32(8));
                    employee.JobID = (reader.IsDBNull(9) ? "" : reader.GetString(9));
                    employee.DepID = (reader.IsDBNull(10) ? 0 : reader.GetInt32(10));

                    Console.WriteLine();
                    employees.Add(employee);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }

            return employees;
        }
        catch
        {
            return new List<Employee>();
        }
    }

    public int Insert(Employee employee)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO employees(id, first_name, last_name, email, phone_number, hire_date, salary, " +
            "comission_pct, manager_id, job_id, departement_id)" +
            "VALUES (@id, @first_name, @last_name, @email, @phone_number, @hire_date, @salary, @commision_pct, @manager_id, " +
            "@job_id, @departement_id)";

        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pFN = new SqlParameter();
            pFN.ParameterName = "@first_name";
            pFN.SqlDbType = SqlDbType.VarChar;
            pFN.Value = employee.FN;
            sqlCommand.Parameters.Add(pFN);

            SqlParameter pLN = new SqlParameter();
            pLN.ParameterName = "@last_name";
            pLN.SqlDbType = SqlDbType.VarChar;
            pLN.Value = employee.LN;
            sqlCommand.Parameters.Add(pLN);

            SqlParameter pEmail = new SqlParameter();
            pEmail.ParameterName = "@email";
            pEmail.SqlDbType = SqlDbType.VarChar;
            pEmail.Value = employee.Email;
            sqlCommand.Parameters.Add(pEmail);

            SqlParameter pPhoneNumber = new SqlParameter();
            pPhoneNumber.ParameterName = "@phone_number";
            pPhoneNumber.SqlDbType = SqlDbType.VarChar;
            pPhoneNumber.Value = employee.Phone;
            sqlCommand.Parameters.Add(pPhoneNumber);

            SqlParameter pHire = new SqlParameter();
            pHire.ParameterName = "@hire_date";
            pHire.SqlDbType = SqlDbType.DateTime;
            pHire.Value = employee.Hire;
            sqlCommand.Parameters.Add(pHire);

            SqlParameter pSalary = new SqlParameter();
            pSalary.ParameterName = "@salary";
            pSalary.SqlDbType = SqlDbType.Int;
            pSalary.Value = employee.Salary;
            sqlCommand.Parameters.Add(pSalary);

            SqlParameter pCom = new SqlParameter();
            pCom.ParameterName = "@comission_pct";
            pCom.SqlDbType = SqlDbType.Decimal;
            pCom.Value = employee.Com;
            sqlCommand.Parameters.Add(pCom);

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

    public int Update(Employee employee)
    {
        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "Update employees set first_name=@first_name, last_name=@last_name, " +
            "email=@email, phone_number=@phone_number, hire_date=@hire_date, salary=@salary," +
            "comission_pct=@comission_pct, manager_id=@manager_id, job_id=@job_id, departement_id=@departement_id where id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            //Set paramaeter value
            sqlCommand.Parameters.AddWithValue("@id", Id);
            sqlCommand.Parameters.AddWithValue("@first_name", FN);
            sqlCommand.Parameters.AddWithValue("@last_name", LN);
            sqlCommand.Parameters.AddWithValue("@email", Email);
            sqlCommand.Parameters.AddWithValue("@phone_number", Phone);
            sqlCommand.Parameters.AddWithValue("@hire_date", Hire);
            sqlCommand.Parameters.AddWithValue("@salary", Salary);
            sqlCommand.Parameters.AddWithValue("@comission_pct", Com);
            sqlCommand.Parameters.AddWithValue("@manager_id", ManID);
            sqlCommand.Parameters.AddWithValue("@job_id", JobID);
            sqlCommand.Parameters.AddWithValue("@departement_id", DepID);

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
        sqlCommand.CommandText = "Delete from employees where id = (@Id)";

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

    public Employee CariIdEm(int id)
    {
        var employee = new Employee();

        var connection = Connection.Get();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM employees WHERE id=@id";
        sqlCommand.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                Console.Write("Cari Employee dengan ID: " + reader.GetInt32(0));
            }

            reader.Close();
            connection.Close();

            return new Employee();
        }
        catch
        {
            return new Employee();
        }
    }
}
