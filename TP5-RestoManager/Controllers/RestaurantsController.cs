using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoManager.Models.RestosModel;

namespace RestoManager.Controllers;

public class RestaurantsController : Controller
{
    private readonly RestosDbContext _context;

    public RestaurantsController(RestosDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        => View(_context.Restaurants.Include(r => r.LeProprio).ToList());

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        var r = _context.Restaurants.Include(r => r.LeProprio).FirstOrDefault(r => r.CodeResto == id);
        if (r == null) return NotFound();
        return View(r);
    }

    public IActionResult Create()
    {
        ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Restaurant restaurant)
    {
        if (ModelState.IsValid)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom", restaurant.NumProp);
        return View(restaurant);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var r = _context.Restaurants.Find(id);
        if (r == null) return NotFound();
        ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom", r.NumProp);
        return View(r);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Restaurant restaurant)
    {
        if (id != restaurant.CodeResto) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Restaurants.Update(restaurant);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom", restaurant.NumProp);
        return View(restaurant);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        var r = _context.Restaurants.Include(r => r.LeProprio).FirstOrDefault(r => r.CodeResto == id);
        if (r == null) return NotFound();
        return View(r);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var r = _context.Restaurants.Find(id);
        if (r != null) { _context.Restaurants.Remove(r); _context.SaveChanges(); }
        return RedirectToAction(nameof(Index));
    }

    // ─── G.1 : Avis par restaurant (propriétés de navigation) ─────────────────
    public IActionResult AvisParRestaurant()
    {
        var restos = _context.Restaurants
            .Include(r => r.LesAvis)
            .Include(r => r.LeProprio)
            .ToList();
        return View(restos);
    }

    // ─── G.2 : Avis d'un restaurant donné (LINQ) ──────────────────────────────
    public IActionResult AvisDuResto(int codeResto)
    {
        var result = (from a in _context.Avis
                      join r in _context.Restaurants
                      on a.NumResto equals r.CodeResto
                      where r.CodeResto == codeResto
                      select new { a.NomPersonne, a.Note, a.Commentaire, r.NomResto })
                     .ToList();
        ViewBag.NomResto = _context.Restaurants.Find(codeResto)?.NomResto;
        ViewBag.Avis = result;
        return View();
    }

    // ─── G.3 : Restaurants avec note moyenne >= 3.5 (LINQ) ────────────────────
    public IActionResult RestosTopNotes()
    {
        var result = (from r in _context.Restaurants
                      join a in _context.Avis on r.CodeResto equals a.NumResto
                      group a by new { r.CodeResto, r.NomResto, r.Ville } into g
                      where g.Average(x => x.Note) >= 3.5
                      select new
                      {
                          g.Key.CodeResto,
                          g.Key.NomResto,
                          g.Key.Ville,
                          MoyenneNote = g.Average(x => x.Note)
                      }).ToList();
        ViewBag.Restos = result;
        return View();
    }
}
