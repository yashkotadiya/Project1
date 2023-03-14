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

       CardViewModel GetFilterData(string[] city, string[] theme, string[] skill, string[] country, string search, string sortOrder);

        CardViewModel GetMissionCard();
        
        List<City> GetCityByCountryName(int id);
    }
}
