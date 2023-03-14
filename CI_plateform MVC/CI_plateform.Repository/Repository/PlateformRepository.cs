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
            int pageSize = 1;
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

           /* data.Missions = mission.Skip((1 - 1) * pageSize).Take(pageSize).ToList();*/

            return data;

        }

        public List<City> GetCityByCountryName(string[] Country)
        {
            var city = _context.Cities.ToList();
            if(Country.Length>0)
            {
                city = city.Where(a => Country.Contains(a.CountryId.ToString())).ToList();
            }
            return city;
        }

        public CardViewModel GetFilterData(string[] city, string[] theme, string[] skill, string[] country, string search, string sortOrder, int pageIndex)
        {
            int pageSize = 1;
            var missionCards = GetMissionCard();
           
            if ( country.Length > 0)
            {
                
                missionCards.Missions = missionCards.Missions.Where(a =>  country.Contains(a.CountryId.ToString())).ToList();

            }
            if (city.Length > 0)
            {
              
              missionCards.Missions = missionCards.Missions.Where(a => city.Contains(a.CityId.ToString())).ToList();
            }
            if (theme.Length > 0)
            {
                   
                   missionCards.Missions = missionCards.Missions.Where(a => theme.Contains(a.ThemeId.ToString())).ToList();
            }
            if (skill.Length > 0)
            {
                
                var x = missionCards.MissionSkills.Where(a => skill.Contains(a.SkillId.ToString())).ToList();
                var list = new List<long>();
                
                foreach(var n in x)
                {
                    list.Add(n.MissionId);

                }
                missionCards.Missions = missionCards.Missions.Where(a => list.Contains(a.MissionId)).ToList();

            }
            if (search != null)
            {
                missionCards.Missions = missionCards.Missions.Where(a => a.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "1":
                    missionCards.Missions = missionCards.Missions.OrderBy(s => s.CreatedAt).ToList();
                    break;
                case "2":
                    missionCards.Missions = missionCards.Missions.OrderByDescending(s => s.CreatedAt).ToList();
                    break;
                case "3":
                    missionCards.Missions = missionCards.Missions.OrderBy(s => s.EndDate).ToList();
                    break;
            }

            if (pageIndex != null)
            {
                missionCards.Missions = missionCards.Missions.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            return missionCards;

        }

        public CardViewModel GetMissionCard()
        {
            List<Mission> mission = _context.Missions.ToList();
            // List<MissionMedium> missionMedia = _context.MissionMedia.ToList();
            List<MissionSkill> missionSkills = _context.MissionSkills.ToList();
            List<MissionTheme> missionThemes = _context.MissionThemes.ToList();
            // List<MissionRating> missionRatings = _context.MissionRatings.ToList();
            List<City> cities = _context.Cities.ToList();
            List<Country> countries = _context.Countries.ToList();
            List<Skill> skills = _context.Skills.ToList();

            CardViewModel missionCards = new CardViewModel();
            {

                missionCards.Missions = mission;
                missionCards.MissionThemes = missionThemes;
                missionCards.MissionSkills = missionSkills;
                missionCards.Skills = skills;
                // missionCards.MissionMedium = missionMedia;
                // missionCards.MissionRating = missionRatings;
                missionCards.Countries = countries;
                missionCards.Cities = cities;
            }
            return missionCards;
        }
    }
}
