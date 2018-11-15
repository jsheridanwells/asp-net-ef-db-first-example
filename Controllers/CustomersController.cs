using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HsSports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HsSports.Controllers
{
    [Produces("application/json")]
        [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly H_Plus_SportsContext _ctx;
        public CustomersController(H_Plus_SportsContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var response =  new ObjectResult(_ctx.Customer)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", _ctx.Customer.Count().ToString());

            return response;
        } 

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult>  GetCustomer([FromRoute] int id)
        {
            if (CustomerExists(id))
            {
                var customer = await _ctx.Customer.SingleOrDefaultAsync(c => c.CustomerId == id);
                return Ok(customer);
            }
            else 
            {
                return NotFound();
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _ctx.Customer.AddAsync(customer);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            _ctx.Entry(customer).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customer = await _ctx.Customer.SingleOrDefaultAsync(c => c.CustomerId == id);
            _ctx.Customer.Remove(customer);
            await _ctx.SaveChangesAsync();
            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _ctx.Customer.Any(c => c.CustomerId == id);
        }
        
    }
}
