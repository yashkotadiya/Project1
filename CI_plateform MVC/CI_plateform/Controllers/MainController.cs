using CI_plateform.Models.Models;
using CI_plateform.Models.ViewModels;
using CI_plateform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_plateform.Controllers
{
    public class MainController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly PlateformInterface _PlateformRepository;
        


        public MainController(ILogger<HomeController> logger, PlateformInterface PlateformRepository, CiplateformContext context)
        {
            _logger = logger;
            _PlateformRepository = PlateformRepository;
           
        }

        
        [Route("Main/plateform", Name = "plateform")]
        public async Task<IActionResult> plateform(CardViewModel model)
        
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            var data =  _PlateformRepository.GetCardData(model);
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

        public IActionResult FilterData(string[] cities, string[] themes, string[] skills, string[] countries, string search,string sortOrder)
        {
            var data = _PlateformRepository.GetFilterData(cities, themes, skills, countries, search, sortOrder);
            return PartialView("Gridcard", data);
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
