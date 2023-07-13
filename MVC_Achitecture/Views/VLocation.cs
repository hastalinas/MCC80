using MVC_Achitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Views;

public class VLocation
{
    public void GetAll(List<Location> locations)
    {
        foreach (var location in locations)
        {
            GetById(location);
        }
    }

    public void GetById(Location location)
    {
        Console.WriteLine("Masukkan ID yang ingin dicari : " + location.Id);
        Console.WriteLine("==========================");
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

    public Location InsertMenu()
    {
        Console.Write("Input ID Location: ");
        int inputID = Int32.Parse(Console.ReadLine());
        Console.Write("Input Address: ");
        string inputAddress = Console.ReadLine();
        Console.Write("Input Postal Code: ");
        string inputPos = Console.ReadLine();
        Console.Write("Input City: ");
        string inputCity = Console.ReadLine();
        Console.Write("Input State Province: ");
        string inputProv = Console.ReadLine();
        Console.Write("Input Country ID: ");
        string inputCoun = Console.ReadLine();

        return new Location() { Id= inputID, StreetAdd = inputAddress, PostalCod = inputPos, City=inputCity, StateProp=inputProv,IdCountry=inputCoun};

    }

    public Location UpdateMenu()
    {
        Console.Write("Masukkan ID yang ingin diganti: ");
        int inputId = Int32.Parse(Console.ReadLine());

        Console.Write("Update Street Address         : ");
        string instreet_address = Console.ReadLine();

        Console.Write("Update Postal Code            : ");
        string inpostal_code = Console.ReadLine();

        Console.Write("Update City                   : ");
        string incity = Console.ReadLine();

        Console.Write("Update State Province         : ");
        string instate_province = Console.ReadLine();

        Console.Write("Update Contry Id              : ");
        string incountry_id = Console.ReadLine();

        return new Location
        {
            Id = inputId,
            StreetAdd = instreet_address,
            PostalCod = inpostal_code,
            City = incity,
            StateProp = instate_province,
            IdCountry = incountry_id

        };
    }

    public int DeleteMenu()
    {
        Console.Write("Id region yang ingin dihapus: ");
        int id = Convert.ToInt32(Console.ReadLine());

        return id;
    }
}

