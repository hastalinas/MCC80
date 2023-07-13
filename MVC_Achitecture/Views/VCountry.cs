using MVC_Achitecture.Models;

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
        Console.WriteLine("Masukkan ID :");
        string? inputId = Console.ReadLine();
        Console.WriteLine("Masukkan Nama :");
        string? inputName = Console.ReadLine();
        Console.WriteLine("Masukkan Region ID :");
        int? inputRegID = Convert.ToInt32(Console.ReadLine());


        return new Country
        {
            Id = inputId,
            Name = inputName,
            RegionId = inputRegID.Value,
        };
    }

    public Country UpdateMenu()
    {
        Console.Write("Id yang ingin diubah: ");
        string inputId = Console.ReadLine();
        Console.Write("Nama Country Baru   : ");
        string inputName = Console.ReadLine();
        Console.Write("Nama Country Baru   : ");
        int regid = Convert.ToInt32(Console.ReadLine());

        return new Country
        {
            Id = inputId,
            Name = inputName,
            RegionId = regid
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
