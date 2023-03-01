using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Models.ViewModels;
public partial class ResetViewModel
{

    public string Email { get; set; } = null!;

      public string Token { get; set; } = null!;

      public string Password { get; set; } = null!;

}

