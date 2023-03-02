using CI_plateform.Models.ViewModels;
using CI_plateform.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_plateform.Repository.Repository.Interface
{
    public interface UserInterface
    {
        Task<int> CreateUser(RegisterViewModel model);
        Task<bool> LoginUser(RegisterViewModel model);
        Task<bool> ForgotUserPassword(ForgotViewModel model);

        /*        Task<int> AddPasswordEmail(ResetViewModel model, string token);*/
        Task<int> ResetUserPassword(ResetViewModel model);


      

    }
    
}