using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba_banco_guayaquil.ConectionBD;
using prueba_banco_guayaquil.Models;

namespace prueba_banco_guayaquil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ContextDB _context;

        public RegistrationController(ContextDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            return await _context.Registrations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {
            var registrationItem = await _context.Registrations.FindAsync(id);

            if (registrationItem == null)
            {
                return NotFound();
            }

            return registrationItem;
        }

        [HttpPost]
        public async Task<ActionResult<Registration>> CreateRegistration([FromBody] Registration newRegistration)
        {
            _context.Registrations.Add(newRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistration), new { id = newRegistration.RegistrationId }, newRegistration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistration(int id, [FromBody] Registration updatedRegistration)
        {
            if (id != updatedRegistration.RegistrationId)
            {
                return BadRequest();
            }

            _context.Entry(updatedRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
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
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registrationItem = await _context.Registrations.FindAsync(id);
            if (registrationItem == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registrationItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}
