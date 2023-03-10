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
        public CardViewModel GetCardData(CardViewModel Model)
        {
           var mission = _context.Missions.ToList();
            var skill = _context.Skills.ToList();
            var city = _context.Cities.ToList();
            var country = _context.Countries.ToList();
            var theme = _context.MissionThemes.ToList();
            var missionSkill = _context.MissionSkills.ToList();

            var data = new CardViewModel();
            data.Cities = city;
            data.Countries = country;
            data.Missions = mission;
            data.Skills = skill;
            data.MissionThemes = theme;
            data.MissionSkills = missionSkill;

            return data;

        }

        public List<City> GetCityByCountryName(int id)
        {
            var city = _context.Cities.Where(a => a.CountryId == id).ToList();
            return city;
        }

        public List<Mission> GetSortingMission(string sortOrder)
        {
            var mission = _context.Missions.ToList();
            switch (sortOrder)
            {
                case "Newest":
                    mission = _context.Missions.OrderBy(s => s.CreatedAt).ToList();
                    break;
                case "Oldest":
                    mission = _context.Missions.OrderByDescending(s => s.CreatedAt).ToList();
                    break;
                case "Deadline":
                    mission = _context.Missions.OrderBy(s => s.EndDate).ToList();
                    break;

            }
            return mission;

        }

       /* public Task<Mission> MissionCarsView(Mission Model)
        {
            var Missions = _context.Missions.ToList();
            return Missions;
        }*/
    }
}
