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
        return View();
    } 


    public IActionResult Home()
    {
        HttpResponseMessage response = httpClient.GetAsync("dolci").Result;
        List<dolci> sweets = new List<dolci>(); 
        List<DPS.Models.Sweet> result = new List<DPS.Models.Sweet>(); 
        string data = response.Content.ReadAsStringAsync().Result;
        sweets = JsonSerializer.Deserialize<List<dolci>>(data); 

        foreach (dolci item in sweets)
        {
            Sweet sweet = new Sweet();
            sweet.ID = item.id;
            sweet.Name = item.nome;
            sweet.Quantity = item.quantita;
            sweet.Price = item.prezzo;
            result.Add(sweet);
        }
        return View(result);
    }
    
    public IActionResult SweetView()
    { 
        return View();
    } 

    [HttpGet]
    public JsonResult GetSweet()
    {
        HttpResponseMessage response = httpClient.GetAsync("dolci").Result;
        List<dolci> sweets = new List<dolci>();  
        string data = response.Content.ReadAsStringAsync().Result;
        sweets = JsonSerializer.Deserialize<List<dolci>>(data); 
 
        return Json(sweets);
    }
    [HttpGet]
    public JsonResult GetSweetById(string id)
    {
        HttpResponseMessage response = httpClient.GetAsync("dolci/" + id).Result;
        dolci sweets = new dolci();  
        string data = response.Content.ReadAsStringAsync().Result;
        sweets = JsonSerializer.Deserialize<dolci>(data); 
 
        return Json(sweets);
    }

    [HttpPost]
    public JsonResult CreateSweet(dolci sweet)
    {  
        var json = JsonSerializer.Serialize<dolci>(sweet);
        var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = httpClient.PostAsync("dolci", content).Result; 
        
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Json("Saved");
        }
 
        return Json(response.StatusCode);
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