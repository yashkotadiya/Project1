
using CI_plateform.Models;
using CI_plateform.Models.Models;
using CI_plateform.Models.ViewModels;
using CI_plateform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace CI_plateform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BaseRepository _userRepository;
        private readonly CiplateformContext _context;


        public HomeController(ILogger<HomeController> logger, BaseRepository userRepository, CiplateformContext context)
        {
            _logger = logger;
            _userRepository = userRepository;
            _context = context;
        }

        [Route("", Name = "Default")]
        [Route("Home/login", Name = "login")]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        [Route("Home/login", Name = "login")]
        public async Task<IActionResult> login(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                bool x = await _userRepository.LoginUser(model);
                if (x == true)
                {
                    return RedirectToAction("plateform", "Main");
                }

            }
            return View(model);
        }
        public IActionResult forgot()
        {
            return View();
        }




        [HttpPost]
        [Route("Home/forgot", Name = "forgot")]
        public async Task<IActionResult> forgot(ForgotViewModel model)
        {
            if (ModelState.IsValid)
            {
                /*            int x = await _userRepository.ForgotUserPass(model);
                            if (x == 1)
                            {
                                return RedirectToAction("resetpass", "Home");
                            }
                        }*/
                var account = _context.Users.Where(a => a.Email == model.Email).FirstOrDefault();
                if (account != null)
                {

                    string token = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, token);
                   
                    var user1 = new PasswordReset()
                    {
                        Email = model.Email,
                        Token = token,
                    };
                    
                    _context.PasswordResets.Add(user1);
                    _context.SaveChanges();
                  

                }


            }
            return View(model);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string Email, string Token)
        {
            /*var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);*/


            var link = Url.ActionLink("resetpass", "Home", new { Email = Email, Token = Token });

            var fromEmail = new MailAddress("harpesh123456@outlook.com", "CI Platform"); ;
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "Vh@12345";

            string subject = "Reset Password";
            string body = "Hi,</br> </br> We got request for reset your account password.Please click on the below link to reset your password" + "<br/><br/><a href=" + link + ">Reset Password link</a>";



            var smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }





        public IActionResult register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                await _userRepository.CreateUser(model);
                return RedirectToRoute("login");
            }

            ViewData["ModelState"] = "Model state invalid.";
            return View(model);
        }

      /*  public IActionResult resetpass()
        {
            return View();
        }*/

        [HttpGet]
        [Route("Home/resetpass", Name = "reset")]
        public IActionResult resetpass(string Email, string Token)
        {
            ResetViewModel rp = new ResetViewModel()
            {
                Email = Email,
                Token = Token,
            };
            return View();

        }
        [HttpPost]

        [Route("Home/resetpass", Name = "reset")]
        public IActionResult resetpass(ResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkemail = _context.PasswordResets.Where(u => u.Email == model.Email && u.Token == model.Token).OrderBy(u => u.Id).LastOrDefault();
                if (checkemail != null)
                {
                    var updatepass = _context.Users.Where(u => u.Email == model.Email).FirstOrDefault();

                    if (updatepass != null)
                    {
                        updatepass.Password = model.Password;
                        updatepass.UpdatedAt = DateTime.Now;

                        _context.Users.Update(updatepass);
                        _context.SaveChanges();
                        return RedirectToAction("login");
                    }
                    else
                    {
                        return View(model);
                    }

                }
                else
                {
                    return View(model);
                }

            }
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}