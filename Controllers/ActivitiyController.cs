using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba_banco_guayaquil.ConectionBD;
using prueba_banco_guayaquil.Models;

namespace prueba_banco_guayaquil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiyController : ControllerBase
    {
        private readonly ContextDB _context;

        public ActivitiyController(ContextDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activityItem = await _context.Activities.FindAsync(id);

            if (activityItem == null)
            {
                return NotFound();
            }

            return activityItem;
        }

        [HttpPost]
        public async Task<ActionResult<Activity>> CreateActivity([FromBody] Activity newActivity)
        {
            _context.Activities.Add(newActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivity), new { id = newActivity.ActivityId }, newActivity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] Activity updatedActivity)
        {
            if (id != updatedActivity.ActivityId)
            {
                return BadRequest();
            }

            _context.Entry(updatedActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activityItem = await _context.Activities.FindAsync(id);
            if (activityItem == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activityItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityId == id);
        }
    }
}
