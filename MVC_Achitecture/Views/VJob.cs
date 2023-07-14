using MVC_Achitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Views;

public class VJob
{
    public void GetAll(List<Job> jobs)
    {
        foreach (var job in jobs)
        {
            GetById(job);
        }
    }
    public void GetById(Job job)
    {
        Console.WriteLine("Id Job (Kode Huruf): " + job.Id);
        Console.WriteLine("Job Name: " + job.Title);
        Console.WriteLine("Min Salary : "+ job.MinSal);
        Console.WriteLine("Max Salary : " + job.MaxSal);
        Console.WriteLine("==========================");
    }

    public string CariId()
    {
        Console.WriteLine("Masukkan ID Job (Kode Huruf)");
        string id = Console.ReadLine();
        return id;
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

    public Job InsertMenu()
    {
        Console.Write("Tambah ID: ");
        string inId = Console.ReadLine();
        Console.Write("Tambah Title: ");
        string inTitle = Console.ReadLine();
        Console.Write("Tambah Min Salary: ");
        int inMinSal = Int32.Parse(Console.ReadLine());
        Console.Write("Tambah Max Salary: ");
        int inMaxSal = Int32.Parse(Console.ReadLine());
       
        return new Job
        {
            Id = inId,
            Title = inTitle,
            MinSal = inMinSal,
            MaxSal = inMaxSal
        };
    }

    public Job UpdateMenu()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        string id = Console.ReadLine();
        Console.Write("Ubah Title                    : ");
        string title = Console.ReadLine();
        Console.Write("Ubah Gaji Minimal             : ");
        int min_salary = Int32.Parse(Console.ReadLine());
        Console.Write("Ubah Gaji Maksimal            : ");
        int max_salary = Int32.Parse(Console.ReadLine());

        return new Job
        {
            Id = id,
            Title = title,
            MinSal = min_salary,
            MaxSal = max_salary
        };
    }

    public int DeleteMenu()
    {
        Console.Write("Id job yang ingin dihapus : ");
        int id = Int32.Parse(Console.ReadLine());

        return id;

    }
}
