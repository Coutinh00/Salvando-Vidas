using AbrigoHub.Core.Entities;
using AbrigoHub.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbrigoHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbrigosController : ControllerBase
    {
        private readonly AbrigoHubContext _context;

        public AbrigosController(AbrigoHubContext context)
        {
            _context = context;
        }

        // GET: api/Abrigos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abrigo>>> GetAbrigos()
        {
            return await _context.Abrigos
                .Include(a => a.Usuario)
                .Include(a => a.Necessidades)
                .Include(a => a.Recursos)
                .ToListAsync();
        }

        // GET: api/Abrigos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Abrigo>> GetAbrigo(int id)
        {
            var abrigo = await _context.Abrigos
                .Include(a => a.Usuario)
                .Include(a => a.Necessidades)
                .Include(a => a.Recursos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (abrigo == null)
            {
                return NotFound();
            }

            return abrigo;
        }

        // POST: api/Abrigos
        [HttpPost]
        public async Task<ActionResult<Abrigo>> PostAbrigo(Abrigo abrigo)
        {
            _context.Abrigos.Add(abrigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAbrigo), new { id = abrigo.Id }, abrigo);
        }

        // PUT: api/Abrigos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbrigo(int id, Abrigo abrigo)
        {
            if (id != abrigo.Id)
            {
                return BadRequest();
            }

            _context.Entry(abrigo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbrigoExists(id))
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

        // DELETE: api/Abrigos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbrigo(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null)
            {
                return NotFound();
            }

            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Abrigos/5/necessidades
        [HttpGet("{id}/necessidades")]
        public async Task<ActionResult<IEnumerable<AbrigoNecessidade>>> GetAbrigoNecessidades(int id)
        {
            var necessidades = await _context.AbrigosNecessidades
                .Where(n => n.AbrigoId == id)
                .ToListAsync();

            if (!necessidades.Any())
            {
                return NotFound();
            }

            return necessidades;
        }

        // GET: api/Abrigos/5/recursos
        [HttpGet("{id}/recursos")]
        public async Task<ActionResult<IEnumerable<AbrigoRecurso>>> GetAbrigoRecursos(int id)
        {
            var recursos = await _context.AbrigosRecursos
                .Where(r => r.AbrigoId == id)
                .ToListAsync();

            if (!recursos.Any())
            {
                return NotFound();
            }

            return recursos;
        }

        private bool AbrigoExists(int id)
        {
            return _context.Abrigos.Any(e => e.Id == id);
        }
    }
} 