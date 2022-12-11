using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNetWebApp.Data;
using MyNetWebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNetWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            this._context = context;
        }
        // GET: /<controller>/
        public IActionResult Index() // Controller
        {
            List<Club> clubs = _context.Clubs.ToList(); // get data from Model
            return View(clubs); // show data in View
        }
    }
}

