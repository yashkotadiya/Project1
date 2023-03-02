using CI_plateform.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Repository.Repository.Interface
{
    public interface PlateformInterface
    {
        Task<List<City>> DisplayCityAndCountry(City Model);
    }
}
