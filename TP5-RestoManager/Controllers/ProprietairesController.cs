using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoManager.Models.RestosModel;

namespace RestoManager.Controllers;

public class ProprietairesController : Controller
{
    private readonly RestosDbContext _context;

    public ProprietairesController(RestosDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        => View(_context.Proprietaires.ToList());

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        var p = _context.Proprietaires.Find(id);
        if (p == null) return NotFound();
        return View(p);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Proprietaire proprietaire)
    {
        if (ModelState.IsValid)
        {
            _context.Proprietaires.Add(proprietaire);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(proprietaire);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var p = _context.Proprietaires.Find(id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Proprietaire proprietaire)
    {
        if (id != proprietaire.Numero) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Proprietaires.Update(proprietaire);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(proprietaire);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        var p = _context.Proprietaires.Find(id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var p = _context.Proprietaires.Find(id);
        if (p != null) { _context.Proprietaires.Remove(p); _context.SaveChanges(); }
        return RedirectToAction(nameof(Index));
    }

    // ─── Jointure G.2 : Avis d'un restaurant donné (via LINQ) ───────────────
    public IActionResult ProdsAndTheirMovies()
    {
        var props = _context.Proprietaires
            .Include(p => p.LesRestos)
            .ToList();
        return View(props);
    }
}
