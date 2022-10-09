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
            var customer = _customerRepository.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _customerRepository.EditCustomer(customer);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            _customerRepository.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}
