using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HsSports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderItems")]
    public class OrderItemsController : Controller
    {
        private readonly H_Plus_SportsContext _ctx;
        public OrderItemsController(H_Plus_SportsContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult GetOrderItem()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderItem([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostOrderItem([FromBody] Object obj)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutOrderItem([FromRoute] int id, [FromBody] Object obj)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem([FromRoute] int id)
        {
            return Ok();
        }
    }
}
