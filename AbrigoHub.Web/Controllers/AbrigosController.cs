using AbrigoHub.Core.Entities;
using AbrigoHub.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: Abrigos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abrigos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Abrigo abrigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abrigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abrigo);
        }
    }
} 