using MVC_Achitecture.Views;
using MVC_Achitecture.Models;

namespace MVC_Achitecture.Controllers;
public class RegionController
{
    private Region _regionModel;
    private VRegion _regionView;

    public RegionController(Region regionModel, VRegion regionView)
    {
        _regionModel = regionModel;
        _regionView = regionView;
    }

    public void GetAll()
    {
        var result = _regionModel.GetAll();
        if (result.Count is 0)
        {
            _regionView.DataEmpty();
        }
        else
        {
            _regionView.GetAll(result);
        }
    }

    public void GetById()
    {
        //var result = _regionView.GetById(regionId);
        int id = _regionView.CariId();
        var region = _regionModel.CariIdReg(id);
        if (region == null)
        {
            _regionView.DataEmpty(); //DisplayRegion(region);
        }
        else
        {
            _regionView.GetById(region);// RegionNotFound();
        }
    }

    public void Insert()
    {
        var region = _regionView.InsertMenu();
        var result = _regionModel.Insert(region);
        switch (result)
        {
            case -1:
                _regionView.Error();
                break;
            case 0:
                _regionView.Failure();
                break;
            default:
                _regionView.Success();
                break;
        }
    }

    public void Update()
    {
        var region = _regionView.UpdateMenu();
        var result = _regionModel.Update(region);

        switch (result)
        {
            case -1:
                _regionView.Error();
                break;
            case 0:
                _regionView.Failure();
                break;
            default:
                _regionView.Success();
                break;
        }
    }

    public void Delete()
    {
        var region = _regionView.DeleteMenu();
        var result = _regionModel.Delete(region);

        switch (result)
        {
            case -1:
                _regionView.Error();
                break;
            case 0:
                _regionView.Failure();
                break;
            default:
                _regionView.Success();
                break;
        }
    }
}

