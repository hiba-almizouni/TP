using Microsoft.AspNetCore.Mvc;
using CinemaManager.Models.Cinema;

namespace CinemaManager.Controllers;

public class ProducersController : Controller
{
    private readonly CinemaDbContext _context;

    public ProducersController(CinemaDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        => View(_context.Producers.ToList());

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        var producer = _context.Producers.Find(id);
        if (producer == null) return NotFound();
        return View(producer);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Producer producer)
    {
        if (ModelState.IsValid)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(producer);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var producer = _context.Producers.Find(id);
        if (producer == null) return NotFound();
        return View(producer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Producer producer)
    {
        if (id != producer.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Producers.Update(producer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(producer);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        var producer = _context.Producers.Find(id);
        if (producer == null) return NotFound();
        return View(producer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var producer = _context.Producers.Find(id);
        if (producer != null)
        {
            _context.Producers.Remove(producer);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
