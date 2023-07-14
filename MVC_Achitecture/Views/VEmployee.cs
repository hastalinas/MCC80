using MVC_Achitecture.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Views;

public class VEmployee
{
    public void GetAll(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            GetById(employee);
        }
    }
    public void GetById(Employee employee)
    {
        Console.WriteLine("Id             : " + employee.Id);
        Console.WriteLine("First Name     : " + employee.FN);
        Console.WriteLine("Last Name      : " + employee.LN);
        Console.WriteLine("Email          : " + employee.Email);
        Console.WriteLine("Phone          : " + employee.Phone);
        Console.WriteLine("Hire Date      : " + employee.Hire);
        Console.WriteLine("Salary         : " + employee.Salary);
        Console.WriteLine("Commision      : " + employee.Com);
        Console.WriteLine("Manager Id     : " + employee.ManID);
        Console.WriteLine("Job Id         : " + employee.JobID);
        Console.WriteLine("Departement ID : " + employee.DepID);
        Console.WriteLine();
        Console.WriteLine("==========================");
    }
    public int CariId()
    {
        Console.Write("Cari Id Employee: ");
        int inputId = Convert.ToInt32(Console.ReadLine());
        return inputId;
    }

    public void DataEmpty()
    {
        Console.WriteLine("Data Not Found!");
    }

    public void Success()
    {
        Console.WriteLine("Success!");
    }

    public void Failure()
    {
        Console.WriteLine("Fail, Id not found!");
    }

    public void Error()
    {
        Console.WriteLine("Error retrieving from database!");
    }

    public int Menu()
    {
        Console.WriteLine("1. Create");
        Console.WriteLine("2. Update");
        Console.WriteLine("3. Delete");
        Console.WriteLine("4. Get By Id");
        Console.WriteLine("5. Get All");
        Console.WriteLine("6. Back");
        Console.WriteLine();

        Console.Write("Masukkan Pilihan : ");
        int input = Int32.Parse(Console.ReadLine());
        return input;
    }

    //belum
    public Employee InsertMenu()
    {
        Console.Write("Tambah ID: ");
        int inId = Int32.Parse(Console.ReadLine());
        Console.Write("Tambah First Name: ");
        string inFirst_name = Console.ReadLine();
        Console.Write("Tambah Last Name: ");
        string inLast_name = Console.ReadLine();
        Console.Write("Tambah Email: ");
        string inEmail = Console.ReadLine();

        Console.Write("Tambah Hire Date (YYYY-MM-DD): ");
        string inputHire = Console.ReadLine();
        DateTime inHire_date;
        if (DateTime.TryParseExact(inputHire, "YYYY-MM-DD", CultureInfo.InvariantCulture, DateTimeStyles.None, out inHire_date))
        {
            // Tanggal berhasil di-parse menjadi objek DateTime
            // Gunakan inputSD sesuai kebutuhan Anda
            Console.WriteLine("Hire Date: " + inHire_date);
        }
        else
        {
            // Tanggal gagal di-parse
            Console.WriteLine("Invalid date format!");
        }


        Console.Write("Tambah Salary: ");
        int inSalary = Int32.Parse(Console.ReadLine());
        Console.Write("Tambah Phone Number: ");
        string inPhone_number = Console.ReadLine();
        Console.Write("Tambah Commission: ");
        decimal inCommission_pct = Decimal.Parse(Console.ReadLine());
        Console.Write("Tambah Manager ID: ");
        int inManager_id = Int32.Parse(Console.ReadLine());
        Console.Write("Tambah Job ID: ");
        string inJob_id = Console.ReadLine();
        Console.Write("Tambah Department ID: ");

        return new Employee
        {
            Id = inId,
            FN = inFirst_name,
            LN = inLast_name,
            Email = inEmail,
            Hire = inHire_date,
            Salary = inSalary,
            Phone = inPhone_number,
            Com = inCommission_pct,
            ManID = inManager_id,
            JobID = inJob_id
        };


    }

    public Employee UpdateMenu()
    {
        Console.Write("Input Start Date (yyyy-MM-dd): ");
        string inputSDString = Console.ReadLine();
        DateTime inputSD;

        if (DateTime.TryParseExact(inputSDString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out inputSD))
        {
            // Tanggal berhasil di-parse menjadi objek DateTime
            // Gunakan inputSD sesuai kebutuhan Anda
            Console.WriteLine("Start Date: " + inputSD);
        }
        else
        {
            // Tanggal gagal di-parse
            Console.WriteLine("Invalid date format!");
        }

        Console.Write("Input Employee ID: ");
        int inEmpID = Int32.Parse(Console.ReadLine());

        Console.Write("Input End Date (yyyy-MM-dd): ");
        string inputEDString = Console.ReadLine();
        DateTime inputED;

        if (DateTime.TryParseExact(inputEDString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out inputED))
        {
            // Tanggal berhasil di-parse menjadi objek DateTime
            // Gunakan inputSD sesuai kebutuhan Anda
            Console.WriteLine("End Date: " + inputED);
        }
        else
        {
            // Tanggal gagal di-parse
            Console.WriteLine("Invalid date format!");
        }
        Console.Write("Input Departemen ID: ");
        int inputDepID = Int32.Parse(Console.ReadLine());

        Console.Write("Input Job ID: ");
        string inJobID = Console.ReadLine();


        return new Employee
        {

        };

    }

    public int DeleteMenu()
    {
        Console.Write("Id History yang ingin dihapus: ");
        int id = Convert.ToInt32(Console.ReadLine());

        return id;

    }
}


