using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EshopApi.Models;
using EshopApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EshopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
   // [Authorize]
    public class CustomersController : Controller
    {
        private ICustomerRepository _cusotmerRepository;
        public CustomersController(ICustomerRepository cusotmerRepository)
        {
            _cusotmerRepository = cusotmerRepository;
        }

        [HttpGet]
        //[ResponseCache(Duration = 60)]
        public IActionResult GetCustomers()
        {

            var result = new ObjectResult(_cusotmerRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Count", _cusotmerRepository.GetCustomerCount().ToString());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if(await _cusotmerRepository.HasCustomer(id))
            {
                var customer = await _cusotmerRepository.GetById(id);
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cusotmerRepository.AddCustomer(customer);
            return CreatedAtAction("GetCustomer" ,  new { id = customer.CustomerId} , customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            await _cusotmerRepository.UpdateCustomer(customer);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _cusotmerRepository.DeleteCustomer(id);
            return Ok();
        }


    }
}