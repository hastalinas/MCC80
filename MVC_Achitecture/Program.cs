﻿using MVC_Achitecture.Controllers;
using MVC_Achitecture.Models;
using MVC_Achitecture.Views;
using System.Linq.Expressions;

namespace MVC_Achitecture;

public class Program
{ 
    public static void Main()
    {
        MenuUtama();
    }
    public static void MenuUtama()
    {
        bool ulang = true;
        do
        {
            Console.WriteLine("SHERINA ERIA HASTALINA");
            Console.WriteLine("======= MENU DATABASE HR =======");
            Console.WriteLine("1. Region");
            Console.WriteLine("2. Country");
            Console.WriteLine("3. Location");
            Console.WriteLine("4. Departement");
            Console.WriteLine("5. Employee");
            Console.WriteLine("6. Job");
            Console.WriteLine("7. History");
            Console.WriteLine("8. Keluar");
            Console.WriteLine("===============================");
            Console.Write("Masukkan Pilihan : ");
            //string choice = Console.ReadLine();

            try
            {
                int pilihMenu = Int32.Parse(Console.ReadLine());

                switch (pilihMenu)
                {
                    case 1:
                        MenuReg();
                        break;
                    case 8:
                        ulang = false;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak tersedia");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Input salah!");
            }
        } while (ulang);
    }

    private static void MenuReg()
    {
        Region region = new Region();
        VRegion vRegion = new VRegion();

        RegionController regionController = new RegionController(region, vRegion);

        bool isTrue = true;
        do
        {
            int pilihMenu = vRegion.Menu();
            switch (pilihMenu)
            {
                case 1:
                    regionController.Insert();
                    PressAnyKey();
                    break;
                case 2:
                    regionController.Update();
                    PressAnyKey();
                    break;
                default:
                    InvalidInput();
                    break;

            }
        } while (isTrue);
    }

    private static void MenuCoun()
    {

    }

    private static void MenuLoc()
    {

    }

    private static void InvalidInput()
    {
        Console.WriteLine("Your input is not valid!");
    }
    private static void PressAnyKey()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}