﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNetWebApp.Data;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;
using MyNetWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            this._clubRepository = clubRepository;
            this._photoService = photoService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index() // Controller
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); // get data from Model
            return View(clubs); // show data in View
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubViewModel.Image);
                Club club = new Club
                {
                    Title = clubViewModel.Title,
                    Description = clubViewModel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubViewModel.Address.Street,
                        City = clubViewModel.Address.City,
                        State = clubViewModel.Address.State,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubViewModel);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var editClubViewModel = new EditClubViewModel
            { 
                Id = club.Id,
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                Url = club.Image
            };

            return View(editClubViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit( int id, EditClubViewModel editClubViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club!");
                return View("Edit", editClubViewModel);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(editClubViewModel.Id);
            if(userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }catch(Exception e) 
                {
                    ModelState.AddModelError("", "Could not delete!" + e.Message);
                    return View(editClubViewModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(editClubViewModel.Image);

                var club = new Club
                {
                    Id = id,
                    Title = editClubViewModel.Title,
                    Description = editClubViewModel.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = editClubViewModel.AddressId,
                    Address = editClubViewModel.Address,
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }else
            {
                return View(editClubViewModel); 
            }
        }
    }
}

