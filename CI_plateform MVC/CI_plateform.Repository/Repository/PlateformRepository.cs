using CI_plateform.Models.Models;
using CI_plateform.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Repository.Repository
{
    public class PlateformRepository : PlateformInterface
    {
        private readonly CiplateformContext _context;

        public PlateformRepository(CiplateformContext context)
        {
            _context = context;
        }
        public async Task<List<City>> DisplayCityAndCountry(City Model)
        {
            return  _context.Cities
                .Select(city => new City()
                {
                    CityId = city.CityId,
                    Name = city.Name,
                
                }).ToList();
            
        }

    }
}
