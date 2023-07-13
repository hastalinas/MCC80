using MVC_Achitecture.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Views;

public class VHistory
{
    public void GetAll(List<History> histories)
    {
        foreach (var history in histories)
        {
            GetById(history);
        }
    }
    public void GetById(History history)
    {
        Console.WriteLine("Start Date : " + history.StartDate);
        Console.WriteLine("Employee ID: " + history.EmployeeId);
        Console.WriteLine("End Date   : " + history.EndDate);
        Console.WriteLine("Job        : " + history.JobId);
        Console.WriteLine("==========================");
    }

    public int CariId()
    {
        Console.Write("Cari Id History: ");
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

    public History InsertMenu()
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


        return new History
        {
            StartDate = inputSD,
            EmployeeId = inEmpID,
            EndDate = inputED,
            DepartmentId = inputDepID,
            JobId = inJobID
        };


    }

    public History UpdateMenu()
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


        return new History
        {
            StartDate = inputSD,
            EmployeeId = inEmpID,
            EndDate = inputED,
            DepartmentId = inputDepID,
            JobId = inJobID
        };

    }

    public int DeleteMenu()
    {
        Console.Write("Id History yang ingin dihapus: ");
        int id = Convert.ToInt32(Console.ReadLine());

        return id;

    }
}
