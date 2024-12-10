using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SistemaDeBarbeariaFront.Models;

namespace SistemaDeBarbeariaFront.Controllers
{
       public class AgendamentosController : Controller
    {
        private readonly HttpClient _httpClient;

    public AgendamentosController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133/Controllers/AgendamentoController"); // API base URL
    }

    // Lista todos os agendamentos
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("Agendamentos");
        if (!response.IsSuccessStatusCode) return View(new List<Agendamento>());

        var agendamentos = JsonConvert.DeserializeObject<List<Agendamento>>(await response.Content.ReadAsStringAsync());
        return View(agendamentos);
    }

    // Exibe detalhes de um agendamento
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"Agendamentos/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        var agendamento = JsonConvert.DeserializeObject<Agendamento>(await response.Content.ReadAsStringAsync());
        return View(agendamento);
    }

    // Abre o formulário de criação de agendamento
    public IActionResult Create() => View();

    // Envia um novo agendamento para a API
    [HttpPost]
    public async Task<IActionResult> Create(Agendamento agendamento)
    {
        var response = await _httpClient.PostAsJsonAsync("Agendamentos", agendamento);
        if (!response.IsSuccessStatusCode) return View(agendamento);

        return RedirectToAction(nameof(Index));
    }

    // Abre o formulário de edição para um agendamento
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"Agendamentos/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        var agendamento = JsonConvert.DeserializeObject<Agendamento>(await response.Content.ReadAsStringAsync());
        return View(agendamento);
    }

    // Envia as alterações do agendamento para a API
    [HttpPost]
    public async Task<IActionResult> Edit(Agendamento agendamento)
    {
        var response = await _httpClient.PutAsJsonAsync($"Agendamentos/{agendamento.AgendamentoId}", agendamento);
        if (!response.IsSuccessStatusCode) return View(agendamento);

        return RedirectToAction(nameof(Index));
    }

    // Deleta um agendamento
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"Agendamentos/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();

        return RedirectToAction(nameof(Index));
    }
    }


}