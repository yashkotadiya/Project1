using CI_plateform.Models.Models;
using CI_plateform.Models.ViewModels;
using CI_plateform.Repository.Repository.Interface;

namespace CI_plateform.Repository.Repository
{
    public class UserRepository : UserInterface
    {
        private readonly CiplateformContext _context;

        public UserRepository(CiplateformContext context)
        {
            _context = context;
        }
        public async Task<int> CreateUser(RegisterViewModel model)
        {
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                CityId = model.CityId,
                CountryId = model.CountryId,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<bool> LoginUser(RegisterViewModel model)
        {
            var userExists = _context.Users.Any(u => u.Email == model.Email && u.Password == model.Password);
            return userExists;

        }

        public async Task<bool> ForgotUserPassword(ForgotViewModel model)
        {


            /*  var userExists = _context.Users.Where(a => a.Email == model.Email).FirstOrDefault();*/
            bool userExists = _context.Users.Any(a => a.Email == model.Email);
            return userExists;
        }
      /*  public async Task<int> AddPasswordEmail(ResetViewModel model, string token)
        {
            var user1 = new PasswordReset()
            {
                Email = model.Email,
                Token = token,
            };
            _context.PasswordResets.Add(user1);
            await _context.SaveChangesAsync();
            return 1;
        }
*/
        public async Task<int>  ResetUserPassword(ResetViewModel model)
        {
            var checkemailtoken = _context.PasswordResets.Where(u => u.Email == model.Email && u.Token == model.Token).OrderBy(u => u.Id).LastOrDefault();
                if (checkemailtoken != null)
                {
                    var updatepass = _context.Users.Where(u => u.Email == model.Email).FirstOrDefault();

                    if (updatepass != null)
                    {
                        updatepass.Password = model.Password;
                        updatepass.UpdatedAt = DateTime.Now;

                        _context.Users.Update(updatepass);
                        _context.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }
        }

    }
    
}
