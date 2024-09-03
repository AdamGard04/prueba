using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba_banco_guayaquil.ConectionBD;
using prueba_banco_guayaquil.Models;

namespace prueba_banco_guayaquil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ContextDB _context;

        public UserController(ContextDB context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var userItem = await _context.Users.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        // POST /api/users
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser([FromBody] Users newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, newUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            if (id != updatedUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(updatedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userItem = await _context.Users.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
