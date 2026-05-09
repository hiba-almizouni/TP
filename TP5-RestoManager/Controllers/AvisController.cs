using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestoManager.Models.RestosModel;

namespace RestoManager.Controllers;

public class AvisController : Controller
{
    private readonly RestosDbContext _context;

    public AvisController(RestosDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        => View(_context.Avis.ToList());

    public IActionResult Create()
    {
        ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Avis avis)
    {
        if (ModelState.IsValid)
        {
            _context.Avis.Add(avis);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto", avis.NumResto);
        return View(avis);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var a = _context.Avis.Find(id);
        if (a == null) return NotFound();
        ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto", a.NumResto);
        return View(a);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Avis avis)
    {
        if (id != avis.CodeAvis) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Avis.Update(avis);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto", avis.NumResto);
        return View(avis);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        var a = _context.Avis.Find(id);
        if (a == null) return NotFound();
        return View(a);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var a = _context.Avis.Find(id);
        if (a != null) { _context.Avis.Remove(a); _context.SaveChanges(); }
        return RedirectToAction(nameof(Index));
    }
}
