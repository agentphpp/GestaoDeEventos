using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoEventos.Data;
using GestaoEventos.Models;
//using Microsoft.AspNetCore.Authorization;

namespace GestaoEventos.Controllers;

public class EventosController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventosController(ApplicationDbContext context) => _context = context;
    
    // GET: Eventos

    public async Task<IActionResult> Index()
    {
        var eventos = await _context.Eventos
              .Include(e => e.Categoria)
              .Include(e => e.Local)
              .ToListAsync();

        return View(eventos);
    }

    // Get: Abre a página que cria um evento
    public IActionResult Create()
    {
        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome");
        ViewBag.LocalId = new SelectList(_context.Locais, "Id", "Nome");

        return View();
    }

    // Vamos salvar o evento de fato
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Evento evento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome",evento.CategoriaId);
        ViewBag.LocalId = new SelectList(_context.Locais, "Id", "Nome", evento.LocalId);

        return View(evento);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var evento = await _context.Eventos.FindAsync(id);
        if (evento == null) return NotFound();

        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome",evento.CategoriaId);
        ViewBag.LocalId = new SelectList(_context.Locais, "Id", "Nome",evento.LocalId);

        return View(evento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Evento evento)
    {
        if (id != evento.Id) return NotFound();
        
       if (ModelState.IsValid)
        {
            _context.Update(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome",evento.CategoriaId);
        ViewBag.LocalId = new SelectList(_context.Locais, "Id", "Nome", evento.LocalId);

        return View(evento); 
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var evento = await _context.Eventos
              .Include(e => e.Categoria)
              .Include(e => e.Local)
              .FirstOrDefaultAsync(m => m.Id == id);

        return View(evento);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento != null)
        {
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

}
