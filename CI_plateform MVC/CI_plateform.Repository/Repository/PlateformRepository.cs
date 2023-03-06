using CI_plateform.Models.Models;
using CI_plateform.Models.ViewModels;
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
        public async Task<FilterViewModel> GetFilterData(FilterViewModel Model)
        {
           /* var city = _context.Cities.Where(a => a.CityId == id).ToList();
            return city;*/
            var filter = new FilterViewModel
            {
                Cities = _context.Cities.ToList(),
                Skills = _context.Skills.ToList(),
                Contries = _context.Countries.ToList(),
                MissionThemes = _context.MissionThemes.ToList()
            };
          
                return filter;
          
               /* .Select(city => new City()
                {
                    CityId = city.CityId,
                    Name = city.Name,
                
                }).ToList();*/
            
        }

        public async Task<List<City>> GetCityByCountryName(int id)
        {
            var city = _context.Cities.Where(a => a.CountryId == id).ToList();
            return city;
        }

       /* public Task<Mission> MissionCarsView(Mission Model)
        {
            var Missions = _context.Missions.ToList();
            return Missions;
        }*/
    }
}
