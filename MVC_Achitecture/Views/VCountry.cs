using MVC_Achitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Views;

public class VCountry
{
    public void GetAll(List<Country> countries)
    {
        foreach (var country in countries)
        {
            GetById(country);
        }
    }

    public void GetById(Country country)
    {
        Console.Write("Input ID Country: ");
        Console.Write("Input Country   : ");
        Console.Write("Input ID Region : ");
        Console.WriteLine("=============================");
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

    public void Error()
    {
        Console.WriteLine("Error retrieving from database!");
    }

    public Country InsertMenu()
    {
        Console.WriteLine("Masukkan Nama :");
        string? inputName = Console.ReadLine();

        return new Country
        {
            //Id = inputId,
            Name = inputName
        };
    }

    public Region UpdateMenu()
    {
        Console.Write("Id yang ingin diubah: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nama: ");
        string name = Console.ReadLine();

        return new Country
        {
            Id = id,
            Name = name
        };
    }

    public Country DeleteMenu()
    {
        Console.WriteLine("Id Country yang ingin dihapus: ");
        string id = Console.ReadLine();

        return new Country
        {
            Id = id
        };
    }
}
