using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EshopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EshopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private EShopApi_Context _dbcontext;
        public CustomersController(EShopApi_Context dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            HttpContext.Response.Headers.Add("X-count", _dbcontext.Customer.Count().ToString()) ;
            var response = new ObjectResult(_dbcontext.Customer) { StatusCode = (int)HttpStatusCode.OK };
            return response;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if(_dbcontext.Customer.Any(c => c.CustomerId == id))
            {
                var customer = await _dbcontext.Customer.SingleOrDefaultAsync(a => a.CustomerId == id);
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
            _dbcontext.Customer.Add(customer);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction("GetCustomer" ,  new { id = customer.CustomerId} , customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            _dbcontext.Entry(customer).State =  EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customer = await _dbcontext.Customer.FindAsync(id);
            _dbcontext.Customer.Remove(customer);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }


    }
}