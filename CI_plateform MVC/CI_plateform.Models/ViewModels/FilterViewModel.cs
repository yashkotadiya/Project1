using CI_plateform.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Models.ViewModels
{
    public class FilterViewModel
    {
        public IEnumerable<Country> Contries { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<MissionTheme> MissionThemes { get; set; }
        public IEnumerable<Skill> Skills { get; set; }


        /* public virtual DbSet<Country> Countries { get; set; }
         public virtual DbSet<City> Cities { get; set; }

         public virtual DbSet<MissionTheme> MissionThemes { get; set; }
         public virtual DbSet<Skill> Skills { get; set; }
 */

    }
}
