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

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var editRaceViewModel = new EditRaceViewModel
            {
                Id = race.Id,
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                Url = race.Image
            };

            return View(editRaceViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel editRaceViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race!");
                return View("Edit", editRaceViewModel);
            }

            var userRace = await _raceRepository.GetByIdAsyncNoTracking(editRaceViewModel.Id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Could not delete!" + e.Message);
                    return View(editRaceViewModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(editRaceViewModel.Image);

                var race = new Race
                {
                    Id = id,
                    Title = editRaceViewModel.Title,
                    Description = editRaceViewModel.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = editRaceViewModel.AddressId,
                    Address = editRaceViewModel.Address,
                };
                _raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editRaceViewModel);
            }
        }
    }
}

