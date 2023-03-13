using CI_plateform.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Models.ViewModels
{
    public class CardViewModel
    {
      
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<Mission> Missions { get; set; }
        public List<MissionTheme> MissionThemes { get; set; }
        public List<Skill> Skills { get; set; }
        public List<MissionSkill> MissionSkills { get; set; }
     


    }
}
