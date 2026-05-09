using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;
using SchoolAPI.Repositories;

namespace SchoolAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchoolsRepoController : ControllerBase
{
    private readonly IUniversityRepository _repo;

    public SchoolsRepoController(IUniversityRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("get-all-schools")]
    public IActionResult GetSchools() => Ok(_repo.GetSchools());

    [HttpGet("get-school-by-id/{id}")]
    public IActionResult GetSchool(int id)
    {
        var s = _repo.GetSchoolById(id);
        return s == null ? NotFound() : Ok(s);
    }

    [HttpGet("search-by-name/{name}")]
    public IActionResult SearchByName(string name) => Ok(_repo.GetSchoolsByName(name));

    [HttpPost("create-school")]
    public IActionResult CreateSchool(School school) { _repo.AddSchool(school); return Ok(); }

    [HttpPut("edit-school/{id}")]
    public IActionResult EditSchool(int id, School school)
    {
        if (id != school.Id) return BadRequest();
        _repo.UpdateSchool(school);
        return NoContent();
    }

    [HttpDelete("delete-school/{id}")]
    public IActionResult DeleteSchool(int id) { _repo.DeleteSchool(id); return NoContent(); }
}
