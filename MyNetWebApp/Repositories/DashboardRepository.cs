using System;
using MyNetWebApp.Data;
using MyNetWebApp.Interfaces;
using MyNetWebApp.Models;

namespace MyNetWebApp.Repositories
{
	public class DashboardRepository : IDashboardRepository
	{
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
            _context = context;
            _httpContextAccessor = httpContextAccessor;
		}

        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClub = _context.Clubs.Where(club => club.AppUser.Id == curUser);
            return userClub.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClub = _context.Races.Where(race => race.AppUser.Id == curUser);
            return userClub.ToList();
        }
    }
}

