using CI_plateform.Models.Models;
using CI_plateform.Models.ViewModels;
using CI_plateform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_plateform.Controllers
{
    public class MainController : Controller
    {

        /*  public ActionResult secondnavbar()
          {
              return PartialView("secondnav");
          }*/

        private readonly ILogger<HomeController> _logger;
        private readonly PlateformInterface _PlateformRepository;
        


        public MainController(ILogger<HomeController> logger, PlateformInterface PlateformRepository, CiplateformContext context)
        {
            _logger = logger;
            _PlateformRepository = PlateformRepository;
           
        }

        /* public IActionResult city(City Model) 

         {
             var city = _context.Cities.ToList();
             return View(city);

         }*/

        [Route("Main/plateform", Name = "plateform")]
        public async Task<IActionResult> plateform(CardViewModel model, String sortOrder = "a")
        
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            var data =  _PlateformRepository.GetCardData(model,sortOrder);
            return View(data);
        }

        public  IActionResult GetCity(int CountryId)
        {
            var data = _PlateformRepository.GetCityByCountryName(CountryId);

            return Json(data);
        }
       /* public IActionResult SortingMission(String sortOrder)
        {
            var data = _PlateformRepository.GetSortingMission(sortOrder);
            return Json(data);
        }*/

        public IActionResult FilterData(string[] cities, string[] themes, string[] skills,string[] countries, string search)
        {
            var data = _PlateformRepository.GetFilterData(cities, themes, skills, countries, search);
            return Json(data);
        }

       





        public IActionResult volunteer()
        {
            return View();
        }
        public IActionResult storyListing()
        {
            return View();
        }
    }
}
