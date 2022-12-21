using System;
using MyNetWebApp.Models;

namespace MyNetWebApp.Interfaces
{
	public interface IDashboardRepository
	{
		Task<List<Club>> GetAllUserClubs();
		Task<List<Race>> GetAllUserRaces();
		Task<AppUser> GetUserById(string id);
		Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}

