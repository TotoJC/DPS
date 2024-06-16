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
        return View();
    } 
    
    public IActionResult Buy()
    { 
        HttpResponseMessage response = httpClient.GetAsync("dolci").Result;
        List<dolci> sweets = new List<dolci>();  
        string data = response.Content.ReadAsStringAsync().Result;
        sweets = JsonSerializer.Deserialize<List<dolci>>(data); 
        return View(sweets);
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

    [HttpPut]
    public JsonResult BuySweetById(BuySweet buySweet)
    { 
        var json = JsonSerializer.Serialize<BuySweet>(buySweet);
        var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = httpClient.PatchAsync("dolci/" + buySweet.id.ToString(), content).Result; 
        string data = response.Content.ReadAsStringAsync().Result;  
 
        return Json("Done");
    }

    [HttpPost]
    public JsonResult CreateSweet(dolci sweet)
    {  
        sweet.id = Guid.NewGuid();
        var json = JsonSerializer.Serialize<dolci>(sweet);
        var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = httpClient.PostAsync("dolci", content).Result; 
        
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Json("Saved");
        }
 
        return Json(response.StatusCode);
    }

    [HttpPut]
    public JsonResult UpdateSweet(dolci sweet)
    {   
        var json = JsonSerializer.Serialize<dolci>(sweet);
        var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = httpClient.PatchAsync("dolci/" + sweet.id.ToString(), content).Result; 
        
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Json("Updated");
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

