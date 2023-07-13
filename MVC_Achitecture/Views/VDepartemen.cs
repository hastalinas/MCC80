using MVC_Achitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Views;

public class VDepartemen
{
    public void GetAll(List<Departemen> departements)
    {
        foreach (var departemen in departements)
        {
            GetById(departemen);
        }
    }

    public void GetById(Departemen departemen)
    {
        Console.WriteLine("Id Departemen  : " + departemen.Id);
        Console.WriteLine("Depatemen Name: " + departemen.Name);
        Console.WriteLine("==========================");

    }

    public int CariId()
    {
        Console.Write("Cari Id Departement: ");
        int inputId =Convert.ToInt32(Console.ReadLine());
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

    public Departemen InsertMenu()
    {
        Console.Write("Masukan Nama: ");
        string? inputName = Console.ReadLine();

        return new Departemen
        {
            Id = 0,
            Name = inputName
        };
    }

    public Departemen UpdateMenu()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        int inputId = Int32.Parse(Console.ReadLine());
        Console.Write("Ubah Departement Name: ");
        string inputDep = Console.ReadLine();
        Console.Write("Ubah Location ID: ");
        int inputLocID = Int32.Parse(Console.ReadLine());
        Console.Write("Ubah Manager ID: ");
        int inputManID = Int32.Parse(Console.ReadLine());

        return new Departemen
        {
            Id = inputId,
            Name = inputDep,
            Location_id = inputLocID,
            Manager_id = inputManID
        };
    }

    public int DeleteMenu()
    {
        Console.Write("Id departemen yang ingin dihapus: ");
        int id = Convert.ToInt32(Console.ReadLine());

        return id;

    }
}
