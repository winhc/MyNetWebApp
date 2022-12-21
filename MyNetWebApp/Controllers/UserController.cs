using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;
using MyNetWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var userList = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();

            foreach(AppUser user in userList)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Milage
                };
                result.Add(userViewModel);
            }

            return View(result);
        }


        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Milage,
                    Clubs = user.Clubs,
                    Races = user.Races
                };

            return View(userViewModel);
        }
    }
}

