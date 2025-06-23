using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Testing1.Context;
using Testing1.Models;

namespace Testing1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ProductContext _productContext;

        public ProductController(ProductContext productContext)
        {
            _productContext = productContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existProduct = await _productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct is null)
            {
                return BadRequest();
            }
            return Ok(existProduct);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productContext.Products.ToListAsync();
            return Ok(products);
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var existProduct = await _productContext.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (existProduct is null)
            {
                return NotFound();
            }
            _productContext.Products.Remove(existProduct);
            await _productContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if ( product is null)
            {
                return BadRequest("Mehsulun datalari bos ola bilmez!");
            }
            await _productContext.Products.AddAsync(product);
            await _productContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product newProduct)
        {
            if (newProduct==null || id!=newProduct.Id)
            {
                return BadRequest("Melumatlar duzgun deyil.");
            }
            var existProduct = await _productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct==null)
            {
                return NotFound("Product tapilmadi!");
            }
            existProduct.Name = newProduct.Name;
            existProduct.Description = newProduct.Description;
            existProduct.Price = newProduct.Price;
            await _productContext.SaveChangesAsync();
            return Ok(existProduct);
        }

   
    }
}
