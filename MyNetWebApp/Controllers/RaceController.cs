using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNetWebApp.Data;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;
using MyNetWebApp.Repositories;
using MyNetWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            this._raceRepository = raceRepository;
            this._photoService = photoService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel createRaceViewMovdel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(createRaceViewMovdel.Image);
                Race race = new Race
                {
                    Title = createRaceViewMovdel.Title,
                    Description = createRaceViewMovdel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = createRaceViewMovdel.Address.Street,
                        City = createRaceViewMovdel.Address.City,
                        State = createRaceViewMovdel.Address.State
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload faFiled");
            }
            return View(createRaceViewMovdel);
           
        }
    }
}

