using MVC_Achitecture.Models;
using MVC_Achitecture.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Controllers;

public class LinqController
{
    private Employee _employee;
    private Country _country;
    private Region _region;
    private Location _location;
    private Departemen _departemen;

    public LinqController(Employee employee, Departemen departemen, Country country,  Location location ,Region region)
    {
        _employee = employee;
        _departemen = departemen;
        _country = country;
        _region = region;
        _location = location;
    }
    public void EmployeeByLastName()
    {
        var getEmployee = _employee.GetAll();
        var filtered = getEmployee.Select(e => new {
            FirstName = e.FN,
            LastName = e.LN,
            Email = e.Email
        }).FirstOrDefault(emp => emp.LastName.Contains("Sherin"));

        var filteredQuery = (from e in getEmployee
                             where e.LN.Contains("Sherin")
                             select new
                             {
                                 e.FN,
                                 e.LN,
                             }).ToList();

        Console.WriteLine($"{filtered.FirstName} {filtered.LastName}");

        foreach (var employee in filteredQuery)
        {
            Console.WriteLine($"{employee.FN} {employee.LN}");
            //Console.WriteLine($"{employee.Email}");
        }
    }

    public void DetailCountry()
    {
        var getRegion = _region.GetAll();
        var getCountry = _country.GetAll();
        var getLocation = _location.GetAll();


        var detailCountry = getCountry.Join(getRegion,
                                            c => c.RegionId,
                                            r => r.Id,
                                            (c, r) => new { c, r })
                                      .Join(getLocation,
                                            cr => cr.c.Id,
                                            l => l.IdCountry,
                                            (cr, l) => new {
                                                Id = cr.c.Id,
                                                City = l.City,
                                                Country = cr.c.Name,
                                                Region = cr.r.Name
                                            });

        var detailCountryByQuery = (from c in getCountry
                                    join r in getRegion on c.RegionId equals r.Id
                                    join l in getLocation on c.Id equals l.IdCountry
                                    select new
                                    {
                                        Id = c.Id,
                                        City = l.City,
                                        Country = c.Name,
                                        Region = r.Name
                                    }).ToList();

        foreach (var country in detailCountry)
        {
            Console.WriteLine($"{country.Id} {country.City} {country.Country} {country.Region}");
        }
    }

    public void DetailEmployee()
    {
        var getRegion = _region.GetAll();
        var getCountry = _country.GetAll();
        var getLocation = _location.GetAll();
        var getDepartemen = _departemen.GetAll();
        var getEmployee =  _employee.GetAll();

        //column yang tampil : id, full_name, email, phone, salary, department_name, street_address, country_name, region_name.
        /*        var detailEmployeeByQuery = (from e in getEmployee
                                             join d in getDepartemen on e.DepID equals d.Id
                                             join l in getLocation on d.Location_id equals l.Id
                                             join c in getCountry on l.IdCountry equals c.Id
                                             join r in getRegion on c.RegionId equals r.Id
                                            select new
                                            {
                                                Id = e.Id,
                                                Fullname = e.FN + " " +e.LN,
                                                Email = e.Email,
                                                Phone = e.Phone,
                                                Salary = e.Salary,
                                                Departemen = d.Name,
                                                Street = l.StreetAdd,
                                                Country = c.Name,
                                                Region = r.Name
                                            }).ToList();*/

        var detailEmployeeByQuery = (from e in getEmployee
                             join d in getDepartemen on e.DepID equals d.Id
                             join l in getLocation on d.Location_id equals l.Id
                             join c in getCountry on l.IdCountry equals c.Id
                             join r in getRegion on c.RegionId equals r.Id
                             select new
                             {
                                 Id = e.Id,
                                 fullname = e.FN + " " + e.LN,
                                 email = e.Email,
                                 phone = e.Phone,
                                 salary = e.Salary,
                                 departement = d.Name,
                                 address = l.StreetAdd,
                                 country = c.Name,
                                 region = r.Name
                             }).ToList();

        foreach (var employee in detailEmployeeByQuery)
        {
            Console.WriteLine($" Hello World{employee.Id} {employee.fullname} {employee.email} {employee.phone} {employee.salary}" +
                $" {employee.departement} {employee.address} {employee.country} {employee.region}");
        }
        Console.ReadLine();
    }
}
