using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using HsSports.Models;
using HsSports.Contracts;
using System.Collections.Generic;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _repo;

        public CustomersController(ICustomerRepository repository)
        {
            _repo = repository;
        }

        private async Task<bool> CustomerExists(int id)
        {
            return await _repo.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Customer>))]
        public IActionResult GetCustomer()
        {
            var results = new ObjectResult(_repo.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };         
        
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _repo.GetAll().Count().ToString());

            return Ok(results);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _repo.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            try 
            {
                await _repo.Update(customer);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exists = await CustomerExists(id);

                if (!exists)
                {
                    return NotFound();
                }
                else 
                {
                    throw;
                }
            }
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repo.Add(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _repo.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _repo.Remove(id);

            return Ok(customer);
        }
    }
}
