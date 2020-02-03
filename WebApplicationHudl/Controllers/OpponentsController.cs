using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiHudl.Contracts;
using WebApplicationHudl.Model;

namespace WebApplicationHudl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpponentsController : ControllerBase
    {
        private readonly IOpponentsService _service;

        public OpponentsController(IOpponentsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a list of schedule entries.
        /// </summary>
        /// <example>GET api/opponents</example>
        /// <returns>Returns a 200 OK status code.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<OpponentsItem>> Get()
        {
            var items = _service.GetOpponents();
            return Ok(items);
        }

        /// <summary>
        /// Create a new schedule entry.
        /// </summary>
        /// <example>POST api/opponents</example>
        /// <param name="value">A new schedule entry</param>
        /// <returns>On success, returns 201 Created status code; otherwise 400 Bad Request.</returns>
        [HttpPost]
        public ActionResult Post([FromBody] OpponentsItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (value == null)
            {
                return BadRequest();
            }

            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.GameId }, item);
        }

        /// <summary>
        /// Get a single schedule entry by identifier.
        /// </summary>
        /// <example>GET api/opponents/1234567</example>
        /// <param name="gameId">Game identifier</param>
        /// <returns>On success, returns 200 OK status code; if <paramref name="gameId"/>
        /// is negative, returns 400 Bad Request; otherwise 404 Not Found.</returns>
        [HttpGet("{id}")]
        public ActionResult<OpponentsItem> Get(int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest();
            }

            var item = _service.GetById(gameId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Remove a schedule entry.
        /// </summary>
        /// <param name="gameId">Game identifier</param>
        /// <example>DELETE api/opponents/1234567</example>
        /// <returns>On success, returns 204 No Content status code; if <paramref name="gameId"/>
        /// is negative, returns 400 Bad Request; otherwise 404 Not Found.</returns>
        [HttpDelete("{id}")]
        public ActionResult Remove(int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest();
            }

            var existingItem = _service.GetById(gameId);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(gameId);
            return NoContent();
        }

        /// <summary>
        /// Update a single schedule entry.
        /// </summary>
        /// <param name="value">An existing schedule entry</param>
        /// <example>PUT api/opponents</example>
        /// <returns>On success, returns 200 OK status code; if the schedule entry
        /// doesn't exists, return 404 Not Found; otherwise 400 Bad Request.</returns>
        [HttpPut]
        public ActionResult Put([FromBody] OpponentsItem value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var existingItem = _service.GetById(value.GameId);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Update(value);
            return Ok();
        }
    }
}
