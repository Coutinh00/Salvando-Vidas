using AbrigoHub.Core.Entities;
using AbrigoHub.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbrigoHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbrigoAvaliacoesController : ControllerBase
    {
        private readonly AbrigoHubContext _context;

        public AbrigoAvaliacoesController(AbrigoHubContext context)
        {
            _context = context;
        }

        // GET: api/AbrigoAvaliacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbrigoAvaliacao>>> GetAbrigoAvaliacoes()
        {
            return await _context.AbrigosAvaliacoes
                .Include(a => a.Abrigo)
                .Include(a => a.Usuario)
                .ToListAsync();
        }

        // GET: api/AbrigoAvaliacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AbrigoAvaliacao>> GetAbrigoAvaliacao(int id)
        {
            var avaliacao = await _context.AbrigosAvaliacoes
                .Include(a => a.Abrigo)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return avaliacao;
        }

        // POST: api/AbrigoAvaliacoes
        [HttpPost]
        public async Task<ActionResult<AbrigoAvaliacao>> PostAbrigoAvaliacao(AbrigoAvaliacao avaliacao)
        {
            _context.AbrigosAvaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAbrigoAvaliacao), new { id = avaliacao.Id }, avaliacao);
        }

        // PUT: api/AbrigoAvaliacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbrigoAvaliacao(int id, AbrigoAvaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(avaliacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbrigoAvaliacaoExists(id))
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

        // DELETE: api/AbrigoAvaliacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbrigoAvaliacao(int id)
        {
            var avaliacao = await _context.AbrigosAvaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            _context.AbrigosAvaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/AbrigoAvaliacoes/abrigo/5
        [HttpGet("abrigo/{abrigoId}")]
        public async Task<ActionResult<IEnumerable<AbrigoAvaliacao>>> GetAvaliacoesPorAbrigo(int abrigoId)
        {
            return await _context.AbrigosAvaliacoes
                .Include(a => a.Usuario)
                .Where(a => a.AbrigoId == abrigoId)
                .ToListAsync();
        }

        // GET: api/AbrigoAvaliacoes/media/5
        [HttpGet("media/{abrigoId}")]
        public async Task<ActionResult<double>> GetMediaAvaliacoesPorAbrigo(int abrigoId)
        {
            var media = await _context.AbrigosAvaliacoes
                .Where(a => a.AbrigoId == abrigoId)
                .AverageAsync(a => a.Avaliacao);

            return media;
        }

        private bool AbrigoAvaliacaoExists(int id)
        {
            return _context.AbrigosAvaliacoes.Any(e => e.Id == id);
        }
    }
} 