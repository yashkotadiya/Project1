
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
        private readonly UserInterface _userRepository;
        private readonly CiplateformContext _context;


        public HomeController(ILogger<HomeController> logger, UserInterface userRepository, CiplateformContext context)
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
                bool x = await _userRepository.ForgotUserPassword(model);
                if( x != null)
                {
                    string token = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(model.Email, token);
                    /* await _userRepository.ForgotUserPassword(model);*/
                    var user1 = new PasswordReset()
                    {
                        Email = model.Email,
                        Token = token,
                    };
                    _context.PasswordResets.Add(user1);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> resetpass(ResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var x = await _userRepository.ResetUserPassword(model);
                if(x == 1)
                {
                    return RedirectToAction("login");
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