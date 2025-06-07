using AbrigoHub.Core.Entities;
using AbrigoHub.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbrigoHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbrigoNecessidadesController : ControllerBase
    {
        private readonly AbrigoHubContext _context;

        public AbrigoNecessidadesController(AbrigoHubContext context)
        {
            _context = context;
        }

        // GET: api/AbrigoNecessidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbrigoNecessidade>>> GetAbrigoNecessidades()
        {
            return await _context.AbrigosNecessidades
                .Include(n => n.Abrigo)
                .ToListAsync();
        }

        // GET: api/AbrigoNecessidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AbrigoNecessidade>> GetAbrigoNecessidade(int id)
        {
            var necessidade = await _context.AbrigosNecessidades
                .Include(n => n.Abrigo)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (necessidade == null)
            {
                return NotFound();
            }

            return necessidade;
        }

        // POST: api/AbrigoNecessidades
        [HttpPost]
        public async Task<ActionResult<AbrigoNecessidade>> PostAbrigoNecessidade(AbrigoNecessidade necessidade)
        {
            _context.AbrigosNecessidades.Add(necessidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAbrigoNecessidade), new { id = necessidade.Id }, necessidade);
        }

        // PUT: api/AbrigoNecessidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbrigoNecessidade(int id, AbrigoNecessidade necessidade)
        {
            if (id != necessidade.Id)
            {
                return BadRequest();
            }

            _context.Entry(necessidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbrigoNecessidadeExists(id))
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

        // DELETE: api/AbrigoNecessidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbrigoNecessidade(int id)
        {
            var necessidade = await _context.AbrigosNecessidades.FindAsync(id);
            if (necessidade == null)
            {
                return NotFound();
            }

            _context.AbrigosNecessidades.Remove(necessidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/AbrigoNecessidades/urgentes
        [HttpGet("urgentes")]
        public async Task<ActionResult<IEnumerable<AbrigoNecessidade>>> GetNecessidadesUrgentes()
        {
            return await _context.AbrigosNecessidades
                .Include(n => n.Abrigo)
                .Where(n => n.NivelUrgencia == "urgente" && !n.Atendida)
                .ToListAsync();
        }

        private bool AbrigoNecessidadeExists(int id)
        {
            return _context.AbrigosNecessidades.Any(e => e.Id == id);
        }
    }
} 