using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DPS.Models;
using System.Text.Json;

namespace DPS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private Uri baseAddress =new Uri("http://localhost:3000");
    private readonly HttpClient httpClient;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        httpClient = new HttpClient();
        httpClient.BaseAddress = baseAddress;
    }

    public IActionResult Index()
    {
        HttpResponseMessage response = httpClient.GetAsync("dolci/18375ceb-79b3-4526-91bd-6637ecc4004d").Result;
        Sweet sweet = new Sweet();
        string data = response.Content.ReadAsStringAsync().Result;
        dolci dolci = JsonSerializer.Deserialize<dolci>(data);
        sweet.ID = dolci.id;
        return View(sweet);
    } 

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
