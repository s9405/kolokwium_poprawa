using kolokwium_poprawa.Model;
using kolokwium_poprawa.Requests;
using kolokwium_poprawa.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_poprawa.Services
{
    public interface IDataBaseService
    {

        List<FireFigthersActions> GetFireFigthersActions(int id);
        FireTruckResponse FireTruckToAction(FireTrucksRequest request);
    }
}
