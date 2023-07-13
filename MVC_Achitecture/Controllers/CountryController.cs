using MVC_Achitecture.Models;
using MVC_Achitecture.Views;


namespace MVC_Achitecture.Controllers;

public class CountryController
{
    private Country _countryModel;
    private VCountry _countryView;

    public CountryController(Country countryModel, VCountry countryView)
    {
        _countryModel = countryModel;
        _countryView = countryView;
    }

    public void GetAll()
    {
        var result = _countryModel.GetAll();
        if (result.Count is 0)
        {
            _countryView.DataEmpty();
        }
        else
        {
            _countryView.GetAll(result);
        }
    }

    public void GetId()
    {

    }

    public void Insert()
    {
        var country = _countryView.InsertMenu();
        var result = _countryModel.Insert(country);
        switch (result)
        {
            case -1:
                _countryView.Error();
                break;
            case 0:
                _countryView.Failure();
                break;
            default:
                _countryView.Success();
                break;
        }
    }

    public void Update()
    {
        var country = _countryView.UpdateMenu();
        var result = _countryModel.Update(country);

        switch (result)
        {
            case -1:
                _countryView.Error();
                break;
            case 0:
                _countryView.Failure();
                break;
            default:
                _countryView.Success();
                break;
        }
    }

    public void Delete()
    {
        /*        var region = _regionView.DeleteMenu();
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
                }*/
    }
}

