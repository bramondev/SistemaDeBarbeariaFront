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
   
    public class ClientesController : Controller
    {
private readonly HttpClient _httpClient;

    public ClientesController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133/api/Cliente");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("Clientes");
        var clientes = JsonConvert.DeserializeObject<List<Cliente>>(await response.Content.ReadAsStringAsync());
        return View(clientes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"Clientes/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        var cliente = JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());
        return View(cliente);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        var response = await _httpClient.PostAsJsonAsync("Clientes", cliente);
        if (!response.IsSuccessStatusCode) return View(cliente);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"Clientes/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        var cliente = JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());
        return View(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Cliente cliente)
    {
        var response = await _httpClient.PutAsJsonAsync($"Clientes/{cliente.IdCliente}", cliente);
        if (!response.IsSuccessStatusCode) return View(cliente);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"Clientes/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
    }
