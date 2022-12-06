using Core.Entities;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeConext;
        public ProductsController(StoreContext storeConext)
        {
            _storeConext = storeConext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            
            var products = await _storeConext.Products.ToListAsync();
            return Ok(products);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _storeConext.Products.FindAsync(id);
            return Ok(product);
        }
    }
}