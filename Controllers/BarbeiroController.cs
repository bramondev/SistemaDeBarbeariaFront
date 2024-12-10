using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SistemaDeBarbeariaFront.Models;

namespace SistemaDeBarbeariaFront.Controllers
{
   
    public class BarbeiroController : Controller
    {
        private readonly HttpClient _httpClient;

    public BarbeiroController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133/api/Barbeiro");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("Barbeiro");
        var barbeiro = JsonConvert.DeserializeObject<List<Barbeiro>>(await response.Content.ReadAsStringAsync());
        return View(barbeiro);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"Barbeiro/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        var barbeiro = JsonConvert.DeserializeObject<Barbeiro>(await response.Content.ReadAsStringAsync());
        return View(barbeiro);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Barbeiro barbeiro)
    {
        var response = await _httpClient.PostAsJsonAsync("Barbeiro",barbeiro );
        if (!response.IsSuccessStatusCode) return View(barbeiro);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"Barbeiro/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        var barbeiro = JsonConvert.DeserializeObject<Barbeiro>(await response.Content.ReadAsStringAsync());
        return View(barbeiro);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Barbeiro barbeiro)
    {
        var response = await _httpClient.PutAsJsonAsync($"Barbeiro/{barbeiro.IdBarbeiro}", barbeiro);
        if (!response.IsSuccessStatusCode) return View(barbeiro);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"Barbeiro/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
    }
    
