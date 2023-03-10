using CI_plateform.Models.Models;
using CI_plateform.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Repository.Repository.Interface
{
    public interface PlateformInterface
    {
        CardViewModel GetCardData(CardViewModel Model);

        List<Mission> GetSortingMission(string sortOrder);
        
        List<City> GetCityByCountryName(int id);
    }
}
