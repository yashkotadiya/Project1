﻿using CI_plateform.Models.Models;
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
        public async Task<IActionResult> plateform(FilterViewModel model, int id)
        
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            var data = await _PlateformRepository.GetFilterData(model);
           
            /*    */
            /*   return Json(data);*/
            return View(data);
        }

        public async Task<IActionResult> GetCity(int CountryId)
        {
            var data = await _PlateformRepository.GetCityByCountryName(CountryId);
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
