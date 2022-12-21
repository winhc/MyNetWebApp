using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;
using MyNetWebApp.Repositories;
using MyNetWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var dashboardViewModel = new DashboardViewModel
            {
                Clubs = userClubs,
                Races = userRaces
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserViewModel
            {
                Id = user.Id,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.Address == null ? "" : user.Address.City,
                State = user.Address == null ? "" : user.Address.State
            };
            return View(editUserViewModel);
        }

        private void MapUserEdit(AppUser user, EditUserViewModel editUserViewModel, ImageUploadResult imageUploadResult)
        {
            user.Id = editUserViewModel.Id;
            user.Pace = editUserViewModel.Pace;
            user.Mileage = editUserViewModel.Mileage;
            user.Address = new Address
            {
                City = editUserViewModel.City,
                State = editUserViewModel.State,
            };
            user.ProfileImageUrl = imageUploadResult.Url.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit user!");
                return View("Edit", editUserViewModel);
            }
            var user = await _dashboardRepository.GetUserByIdNoTracking(editUserViewModel.Id);
            if (user != null)
            {
                try
                {
                    if (user.ProfileImageUrl != null)
                    {
                        await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Could not delete!" + e.Message);
                    return View(editUserViewModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);

                // Handle Concurrency Exception
                MapUserEdit(user, editUserViewModel, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editUserViewModel);
            }
            
        }
    }
}

