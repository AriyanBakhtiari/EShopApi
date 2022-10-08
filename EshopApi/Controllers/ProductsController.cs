using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EshopApi.Models;
using EshopApi.Repository;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        

        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> GetProduct()
        {
            return _productRepository.GetAll();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Product = await _productRepository.Find(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Product.ProductId)
            {
                return BadRequest();
            }

           await _productRepository.Update(Product);

           

            return NoContent();
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productRepository.Add(Product);

            return CreatedAtAction("GetProduct", new { id = Product.ProductId }, Product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Product = await _productRepository.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            await _productRepository.Remove(id);

            return Ok(Product);
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productRepository.IsExists(id);
        }
    }
}