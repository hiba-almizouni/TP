using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManager.Models.Cinema;

namespace CinemaManager.Controllers;

public class MoviesController : Controller
{
    private readonly CinemaDbContext _context;

    public MoviesController(CinemaDbContext context)
    {
        _context = context;
    }

    // ─── CRUD de base ────────────────────────────────────────────────────────

    public IActionResult Index()
        => View(_context.Movies.Include(m => m.Producer).ToList());

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        var movie = _context.Movies.Include(m => m.Producer).FirstOrDefault(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie);
    }

    public IActionResult Create()
    {
        ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", movie.ProducerId);
        return View(movie);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var movie = _context.Movies.Find(id);
        if (movie == null) return NotFound();
        ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", movie.ProducerId);
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Movie movie)
    {
        if (id != movie.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", movie.ProducerId);
        return View(movie);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        var movie = _context.Movies.Include(m => m.Producer).FirstOrDefault(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie != null) { _context.Movies.Remove(movie); _context.SaveChanges(); }
        return RedirectToAction(nameof(Index));
    }

    // ─── Jointure par Propriétés de Navigation ───────────────────────────────

    public IActionResult MoviesAndTheirProds()
    {
        var movies = _context.Movies.Include(m => m.Producer).ToList();
        return View(movies);
    }

    // ─── Jointure par ViewModel LINQ ─────────────────────────────────────────

    public IActionResult MoviesAndTheirProds_UsingModel()
    {
        var result = (from m in _context.Movies
                      join p in _context.Producers
                      on m.ProducerId equals p.Id
                      select new ProdMovie
                      {
                          mTitle = m.Title,
                          mGenre = m.Genre,
                          pName  = p.Name,
                          pNat   = p.Nationality
                      }).ToList();
        return View(result);
    }

    // Films d'un producteur donné (passé par son ID)
    public IActionResult MyMovies(int id)
    {
        var result = (from m in _context.Movies
                      join p in _context.Producers
                      on m.ProducerId equals p.Id
                      where p.Id == id
                      select new ProdMovie
                      {
                          mTitle = m.Title,
                          mGenre = m.Genre,
                          pName  = p.Name,
                          pNat   = p.Nationality
                      }).ToList();
        return View(result);
    }

    // ─── Recherche & Filtres LINQ ─────────────────────────────────────────────

    // Recherche par titre (query string ou route)
    public IActionResult SearchByTitle(string? critere)
    {
        var movies = (from m in _context.Movies
                      where critere == null || m.Title.Contains(critere)
                      select m).ToList();
        return View("Index", movies);
    }

    // Recherche par genre
    public IActionResult SearchByGenre(string? critere)
    {
        var movies = (from m in _context.Movies
                      where critere == null || m.Genre.Contains(critere)
                      select m).ToList();
        return View("Index", movies);
    }

    // Double filtrage Genre + Titre avec DropDownList
    public IActionResult SearchBy2(string? genre, string? title)
    {
        var genres = _context.Movies.Select(m => m.Genre).Distinct().ToList();
        genres.Insert(0, "All");
        ViewBag.Genres = new SelectList(genres);

        var movies = _context.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(genre) && genre != "All")
            movies = movies.Where(m => m.Genre.Contains(genre));

        if (!string.IsNullOrEmpty(title))
            movies = movies.Where(m => m.Title.Contains(title));

        return View(movies.ToList());
    }
}
