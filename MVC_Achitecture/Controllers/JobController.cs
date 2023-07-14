using MVC_Achitecture.Models;
using MVC_Achitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Achitecture.Controllers;

public  class JobController
{
    private Job _jobModel;
    private VJob _jobView;

    public JobController(Job jobModel, VJob jobView)
    {
        _jobModel = jobModel;
        _jobView = jobView;
    }

    public void GetAll()
    {
        var result = _jobModel.GetAll();
        if (result.Count is 0)
        {
            _jobView.DataEmpty();
        }
        else
        {
            _jobView.GetAll(result);
        }
    }

    public void GetById()
    {
        var id = _jobView.CariId();
        var job = _jobModel.CariIdJob(id);
        if (job == null)
        {
            _jobView.DataEmpty(); 
        }
        else
        {
            _jobView.GetById(job);
        }
    }

    public void Insert()
    {
        var job = _jobView.InsertMenu();
        var result = _jobModel.Insert(job);
        switch (result)
        {
            case -1:
                _jobView.Error();
                break;
            case 0:
                _jobView.Failure();
                break;
            default:
                _jobView.Success();
                break;
        }
    }

    public void Update()
    {
        var job = _jobView.UpdateMenu();
        var result = _jobModel.Update(job);

        switch (result)
        {
            case -1:
                _jobView.Error();
                break;
            case 0:
                _jobView.Failure();
                break;
            default:
                _jobView.Success();
                break;
        }
    }

    public void Delete()
    {
        var job = _jobView.DeleteMenu();
        var result = _jobModel.Delete(job);

        switch (result)
        {
            case -1:
                _jobView.Error();
                break;
            case 0:
                _jobView.Failure();
                break;
            default:
                _jobView.Success();
                break;
        }
    }

}
