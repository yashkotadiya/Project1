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
        public CardViewModel GetCardData(CardViewModel Model, string sortOrder)
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

        public List<MissionViewModel> GetFilterData(string[] city, string[] theme, string[] skill, string[] country, string search)
        {
            List<MissionViewModel> mission = GetMissionCardsList();
          /*  List<Mission> mission = new List<Mission>();
            List<Mission> missions = _context.Missions.ToList();*/

            if (city.Length > 0)
            {
                mission = mission.Where(a => city.Contains(a.CityId.ToString()) ).ToList();
            }
            if (country.Length > 0)
            {
                mission = mission.Where(a => country.Contains(a.CountryId.ToString()) ).ToList();
            }
            if (theme.Length > 0)
            {
                mission = mission.Where(a => theme.Contains(a.ThemeId.ToString())).ToList();
            }
            if (skill.Length > 0)
            {
                mission = mission.Where(a => skill.Contains(a.SkillId.ToString())).ToList();
            }
            if (search != null)
            {
                mission = mission.Where(a => a.Title.Contains(search)).ToList();
            }

            return mission;

        }
        public List<MissionViewModel> GetMissionCardsList()
        {

            var mission = _context.Missions.ToList();

            List<MissionViewModel> missionListingList = new List<MissionViewModel>();

            foreach (var obj in mission)
            {
                var missionskill = _context.MissionSkills.FirstOrDefault(u => u.MissionId == obj.MissionId);

                MissionViewModel missionListing = new MissionViewModel();

               

                missionListing.MissionId = obj.MissionId;
                missionListing.CityName = GetCityName(obj.CityId);
                missionListing.MissionType = obj.MissionType;
                missionListing.SkillId = missionskill.SkillId;
                missionListing.OrganizationName = obj.OrganizationName;
                missionListing.ShortDescription = obj.ShortDescription;
                missionListing.CityId = obj.CityId;
                missionListing.CountryId = obj.CountryId;
                missionListing.StartDate = obj.StartDate;
                missionListing.EndDate = obj.EndDate;
                missionListing.MediaName = GetMediaNameFromId(missionListing.MissionId);
                missionListing.Title = obj.Title;
                missionListing.ThemeId = (int)obj.ThemeId;
                missionListing.Rating = GetMissionRatings(missionListing.MissionId);
                missionListing.Theme = GetMissionThemes(obj.ThemeId);
                missionListing.Deadline = DeadlineOfMission(obj.MissionId);
                missionListing.TotalSeat = GetTotalSeatsOfMission(obj.MissionId);
                //if (obj.MissionType)
                //{
                //    missionListing.TotalSeat = GetTotalSeat(obj.MissionId);
                //    missionListing.Deadline = GetDeadline(obj.MissionId);
                //}
                //if (!obj.MissionType)
                //{
                //    missionListing.GoalObjectiveText = GetGoalObjectiveText(obj.MissionId);
                //    missionListing.GoalValue = GetGoalValue(obj.MissionId);
                //}
                missionListingList.Add(missionListing);
            }
            return missionListingList;
        }




        /*public List<Mission> GetSortingMission(string sortOrder)
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
*/
        /* public Task<Mission> MissionCarsView(Mission Model)
         {
             var Missions = _context.Missions.ToList();
             return Missions;
         }*/
    }
}
