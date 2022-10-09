using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private CustomerRepository _customerRepository;


        public HomeController()
        {
            _customerRepository =  new CustomerRepository();
        }

        public IActionResult Index()
        {
            return View(_customerRepository.GetAllCustomer());
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
