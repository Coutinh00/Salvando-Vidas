using AbrigoHub.Core.Entities;
using AbrigoHub.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbrigoHub.Web.Controllers
{
    public class AbrigosController : Controller
    {
        private readonly AbrigoHubContext _context;

        public AbrigosController(AbrigoHubContext context)
        {
            _context = context;
        }

        // GET: Abrigos
        public async Task<IActionResult> Index()
        {
            var abrigos = await _context.Abrigos.Include(a => a.Usuario).ToListAsync();
            return View(abrigos);
        }

        // GET: Abrigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abrigo = await _context.Abrigos
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abrigo == null)
            {
                return NotFound();
            }

            return View(abrigo);
        }

        // GET: Abrigos/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UsuarioId"] = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nome");
            return View();
        }

        // POST: Abrigos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Endereco,Cidade,Estado,Cep,Capacidade,OcupacaoAtual,Status,UsuarioId,Latitude,Longitude")] Abrigo abrigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abrigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nome", abrigo.UsuarioId);
            return View(abrigo);
        }

        // GET: Abrigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nome", abrigo.UsuarioId);
            return View(abrigo);
        }

        // POST: Abrigos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Endereco,Cidade,Estado,Cep,Capacidade,OcupacaoAtual,Status,UsuarioId,Latitude,Longitude")] Abrigo abrigo)
        {
            Console.WriteLine($"Tentando editar abrigo com ID: {id}");
            if (id != abrigo.Id)
            {
                Console.WriteLine($"ID do abrigo não corresponde: {id} vs {abrigo.Id}");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState é válido. Tentando atualizar o banco de dados.");
                try
                {
                    _context.Update(abrigo);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Abrigo atualizado com sucesso!");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine($"Erro de concorrência ao atualizar abrigo: {ex.Message}");
                    if (!AbrigoExists(abrigo.Id))
                    {
                        Console.WriteLine("Abrigo não encontrado durante erro de concorrência.");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado ao atualizar abrigo: {ex.Message}");
                    // Log details of the exception for further investigation
                    Console.WriteLine(ex.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("ModelState é inválido. Erros de validação:");
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"- {error.ErrorMessage}");
                    }
                }
            }
            ViewData["UsuarioId"] = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nome", abrigo.UsuarioId);
            return View(abrigo);
        }

        // GET: Abrigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abrigo = await _context.Abrigos
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abrigo == null)
            {
                return NotFound();
            }

            return View(abrigo);
        }

        // POST: Abrigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo != null)
            {
                _context.Abrigos.Remove(abrigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbrigoExists(int id)
        {
            return _context.Abrigos.Any(e => e.Id == id);
        }
    }
} 