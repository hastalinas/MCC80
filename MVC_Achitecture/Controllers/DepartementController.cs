using MVC_Achitecture.Models;
using MVC_Achitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Controllers;

public class DepartementController
{
    private Departemen _departemenModel;
    private VDepartemen _departemenView;

    public DepartementController(Departemen departemenModel, VDepartemen departemenView)
    {
        _departemenModel = departemenModel;
        _departemenView = departemenView;
    }
    public void GetAll()
    {
        var result = _departemenModel.GetAll();
        if (result.Count is 0)
        {
            _departemenView.DataEmpty();
        }
        else
        {
            _departemenView.GetAll(result);
        }
    }

    public void GetById()
    {
        //var result = _regionView.GetById(regionId);
        int id = _departemenView.CariId();
        var departemen = _departemenModel.CariIdDep(id);
        if (departemen == null)
        {
            _departemenView.DataEmpty(); //DisplayRegion(region);
        }
        else
        {
            _departemenView.GetById(departemen);// RegionNotFound();
        }
    }

    public void Insert()
    {
        var departemen = _departemenView.InsertMenu();
        var result = _departemenModel.Insert(departemen);
        switch (result)
        {
            case -1:
                _departemenView.Error();
                break;
            case 0:
                _departemenView.Failure();
                break;
            default:
                _departemenView.Success();
                break;
        }
    }

    public void Update()
    {
        var departemen = _departemenView.UpdateMenu();
        var result = _departemenModel.Update(departemen);

        switch (result)
        {
            case -1:
                _departemenView.Error();
                break;
            case 0:
                _departemenView.Failure();
                break;
            default:
                _departemenView.Success();
                break;
        }
    }

    public void Delete()
    {
        var departemen = _departemenView.DeleteMenu();
        var result = _departemenModel.Delete(departemen);

        switch (result)
        {
            case -1:
                _departemenView.Error();
                break;
            case 0:
                _departemenView.Failure();
                break;
            default:
                _departemenView.Success();
                break;
        }
    }
}
