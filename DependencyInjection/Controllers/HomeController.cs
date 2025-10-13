using System.Diagnostics;
using DependencyInjection.Interface;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
     
    public class HomeController : Controller
    {
        private readonly IEnumerable<IGreeter> _greeter;
        public HomeController(IEnumerable<IGreeter> greeter)
        {
            _greeter = greeter;
        }
        public IActionResult Index()
        {
            var messages = _greeter.Select(g => g.GetMessage());
            ViewBag.Message = messages;
            return View();
        }
        //public IActionResult Weekend()
        //{
        //    var message = _greeter.GetMessage();
        //    ViewBag.Message = message;
        //    return View();
        //}


    }
}
