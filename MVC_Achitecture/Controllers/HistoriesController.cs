using MVC_Achitecture.Models;
using MVC_Achitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Controllers;

public class HistoriesController
{
    private History _historyModel;
    private VHistory _historyView;

    public HistoriesController(History historyModel, VHistory historyView)
    {
        _historyModel = historyModel;
        _historyView = historyView;
    }

    public void GetAll()
    {
        var result = _historyModel.GetAll();
        if (result.Count is 0)
        {
            _historyView.DataEmpty();
        }
        else
        {
            _historyView.GetAll(result);
        }
    }

    public void GetById()
    {
        //var result = _regionView.GetById(regionId);
        int id = _historyView.CariId();
        var history = _historyModel.CariIdHis(id);
        if (history == null)
        {
            _historyView.DataEmpty(); //DisplayRegion(region);
        }
        else
        {
            _historyView.GetById(history);// RegionNotFound();
        }
    }

    public void Insert()
    {
        var history = _historyView.InsertMenu();
        var result = _historyModel.Insert(history);
        switch (result)
        {
            case -1:
                _historyView.Error();
                break;
            case 0:
                _historyView.Failure();
                break;
            default:
                _historyView.Success();
                break;
        }
    }

    public void Update()
    {
        var history = _historyView.UpdateMenu();
        var result = _historyModel.Update(history);

        switch (result)
        {
            case -1:
                _historyView.Error();
                break;
            case 0:
                _historyView.Failure();
                break;
            default:
                _historyView.Success();
                break;
        }
    }

    public void Delete()
    {
        var history = _historyView.DeleteMenu();
        var result = _historyModel.Delete(history);

        switch (result)
        {
            case -1:
                _historyView.Error();
                break;
            case 0:
                _historyView.Failure();
                break;
            default:
                _historyView.Success();
                break;
        }
    }
}
