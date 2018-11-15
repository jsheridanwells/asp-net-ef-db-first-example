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
    [Route("api/Salespersons")]
    public class SalespersonsController : Controller
    {
        private readonly H_Plus_SportsContext _ctx;
        public SalespersonsController(H_Plus_SportsContext ctx)
        {
            _ctx = ctx;
        }   

        [HttpGet]
        public IActionResult GetSalesperson()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetSalesperson([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostSalesperson([FromBody] Object obj)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutSalesperson([FromRoute] int id, [FromBody] Object obj)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSalesperson([FromRoute] int id)
        {
            return Ok();
        }
    }
}
