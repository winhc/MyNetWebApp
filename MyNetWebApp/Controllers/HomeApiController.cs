using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;

namespace MyNetWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController : ControllerBase
    {
        private readonly IClubRepository _clubRepository;
        public HomeApiController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        [HttpGet("{title}")]
        public async Task<IEnumerable<Club>?> getClubsByTitle(string title)
        {
            var clubs = await _clubRepository.GetClubsByTitle(title);
            return clubs;
        }
    }
}
