using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CustomerRepository _customerRepository;


        public HomeController()
        {
            _customerRepository =  new CustomerRepository();
        }

        public IActionResult Index()
        {
            var token = User.FindFirst("AccessToken")?.Value ;
            return View(_customerRepository.GetAllCustomer(token));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var token = User.FindFirst("AccessToken")?.Value;
            var customer = _customerRepository.GetCustomerById(id,token );
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var token = User.FindFirst("AccessToken")?.Value;
            _customerRepository.EditCustomer(customer,token);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var token = User.FindFirst("AccessToken")?.Value;
            _customerRepository.DeleteCustomer(id,token);
            return RedirectToAction("Index");
        }
    }
}
