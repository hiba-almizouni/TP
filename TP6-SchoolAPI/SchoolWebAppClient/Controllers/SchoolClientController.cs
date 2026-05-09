using Microsoft.AspNetCore.Mvc;
using SchoolWebAppClient.Models;
using System.Net.Http.Json;

namespace SchoolWebAppClient.Controllers;

public class SchoolClientController : Controller
{
    private readonly HttpClient _client;

    public SchoolClientController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("SchoolAPI");
    }

    public async Task<IActionResult> GetAllSchools()
    {
        var resp = await _client.GetAsync("api/SchoolsRepo/get-all-schools");
        if (resp.IsSuccessStatusCode)
        {
            var schools = await resp.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>();
            return View(schools);
        }
        return View();
    }

    public async Task<IActionResult> GetSchoolById(int id)
    {
        var resp = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");
        if (resp.IsSuccessStatusCode)
            return View(await resp.Content.ReadFromJsonAsync<SchoolClient>());
        return View();
    }

    public async Task<IActionResult> GetSchoolByName(string name)
    {
        var resp = await _client.GetAsync($"api/SchoolsRepo/search-by-name/{name}");
        if (resp.IsSuccessStatusCode)
            return View(await resp.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>());
        return View();
    }

    public IActionResult CreateSchool() => View();

    [HttpPost]
    public async Task<IActionResult> CreateSchool(SchoolClient school)
    {
        var resp = await _client.PostAsJsonAsync("api/SchoolsRepo/create-school", school);
        if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(GetAllSchools));
        return View();
    }

    public async Task<IActionResult> EditSchool(int id)
    {
        var resp = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");
        if (resp.IsSuccessStatusCode)
            return View(await resp.Content.ReadFromJsonAsync<SchoolClient>());
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditSchool(SchoolClient school)
    {
        var resp = await _client.PutAsJsonAsync($"api/SchoolsRepo/edit-school/{school.Id}", school);
        if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(GetAllSchools));
        return View();
    }

    public async Task<IActionResult> DeleteSchool(int id)
    {
        var resp = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");
        if (resp.IsSuccessStatusCode)
            return View(await resp.Content.ReadFromJsonAsync<SchoolClient>());
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSchool(SchoolClient school)
    {
        var resp = await _client.DeleteAsync($"api/SchoolsRepo/delete-school/{school.Id}");
        if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(GetAllSchools));
        return View();
    }
}
