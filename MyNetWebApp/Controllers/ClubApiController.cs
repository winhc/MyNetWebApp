using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;
using MyNetWebApp.Repositories;

namespace MyNetWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubApiController : ControllerBase
    {
        private readonly IClubRepository _clubRepository;

        public ClubApiController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _clubRepository.GetAll();
        }

    }
}

