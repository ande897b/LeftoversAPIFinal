using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLeftOversClubTestApi.Data;
using TheLeftOversClubTestApi.Models;
using static TheLeftOversClubTestApi.Data.ProductContext;

namespace TheLeftOversClubTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ProductContext _context;
        string tempAdvancedDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris id suscipit orci, sit amet auctor lorem. In luctus augue ex, at lacinia sapien aliquet vitae. Donec ut dignissim enim, egestas eleifend elit. Sed et arcu consequat, porttitor augue ut, commodo mauris. Aliquam eu ex ex. Sed ullamcorper purus magna, at aliquam tellus malesuada sed. Pellentesque rhoncus dui ac ex viverra, non scelerisque augue dictum. Donec quis sollicitudin ante. Aenean tortor nunc, finibus nec dapibus eget, vestibulum ut ex. Praesent id aliquam erat. Donec nibh diam, fermentum facilisis pellentesque et, fringilla nec nunc. Mauris condimentum tristique neque tincidunt rutrum. Sed mattis nunc ac ligula scelerisque luctus. Proin porttitor velit eget orci porttitor, nec lacinia eros fermentum.Nullam sit amet condimentum enim.Praesent scelerisque erat ac libero posuere feugiat.Phasellus tristique dignissim velit vel pharetra. Vestibulum feugiat est nec eros fringilla, vel sodales eros viverra.Fusce quam risus, fringilla consequat ipsum quis, mollis sagittis erat. Proin vestibulum feugiat mauris, molestie sodales metus sagittis nec. Phasellus luctus fermentum tellus ut convallis. Morbi rutrum libero odio, sit amet gravida metus mollis a.Aenean a ullamcorper augue, sit amet varius justo.Vivamus et blandit augue, in commodo ex. Nam arcu odio, convallis id dolor eu, ornare fringilla nisi. Integer quis urna sit amet libero elementum consequat a a lacus.Aliquam hendrerit dictum nibh, sit amet ullamcorper lorem efficitur non.Sed vitae luctus lectus. Nullam pharetra ante quis porta sodales";

        public ValuesController(ProductContext context)
        {
            _context = context;
            if (_context.products.Count() == 0)
            {
                _context.Add(new Product { Description = "Dette er produktet til dig med ordblindhed", Price = 699.99, AdvancedDescription = tempAdvancedDescription, Picture = "https://dyslexiaida.org/wp-content/uploads/2016/05/Not-Stupid-Not-Lazy-Cover-Final-sm.jpg" });
                _context.Add(new Product { Description = "Dette er produktet til dig med svagt syn", Price = 1250, AdvancedDescription = tempAdvancedDescription, Picture = "https://res.cloudinary.com/liingo/image/upload/c_fill,g_center,h_339,w_990,q_85/754317164787_2.jpg" });
                _context.Add(new Product { Description = "Dette er et testprodukt", Price = 69, AdvancedDescription = tempAdvancedDescription, Picture = "https://images.sftcdn.net/images/t_app-cover-l,f_auto/p/ce2ece60-9b32-11e6-95ab-00163ed833e7/260663710/the-test-fun-for-friends-screenshot.jpg" });
                _context.SaveChanges();
            }
            
        }


        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.products.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
