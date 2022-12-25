using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;
using MyNetWebApp.Helpers;
using MyNetWebApp.ViewModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Xml.Linq;

namespace MyNetWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IClubRepository _clubRepository;

    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
    {
        _logger = logger;
        _clubRepository = clubRepository;
    }

    public async Task<IActionResult> Index()
    {
        var ipInfo = new IPInfo();
        var homeViewModel = new HomeViewModel();
        try
        {
            {
                string url = "https://ipinfo.io?token=0fe6c599052543";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
                }
                return View(homeViewModel);
            }
        }
        catch(Exception e)
        {
            homeViewModel.Clubs = null;
        }
        
        return View();
    }

    public async Task<IActionResult> SearchClubsByTitle(string title)
    {
        var homeViewModel = new HomeViewModel();
        string apiUrl = $"https://localhost:7293/api/HomeApi/{title}";
        HttpClient httpClient = new HttpClient();

        HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result;
        if (response.IsSuccessStatusCode)
        {
            homeViewModel.Clubs = JsonConvert.DeserializeObject<IEnumerable<Club>>(response.Content.ReadAsStringAsync().Result);
            return View("Index", homeViewModel);
        }
        else
        {
            return View("Index");
        }

    }

    public async Task<IActionResult> Search(string title)
    {
        var homeViewModel = new HomeViewModel();

        var clubs = await _clubRepository.GetClubByTitle(title);
        
        if (clubs is null) return View("Index");
        homeViewModel.Clubs = clubs;
        return View("Index", homeViewModel);
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

