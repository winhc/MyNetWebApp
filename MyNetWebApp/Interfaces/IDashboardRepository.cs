using System;
using MyNetWebApp.Models;

namespace MyNetWebApp.Interfaces
{
	public interface IDashboardRepository
	{
		Task<List<Club>> GetAllUserClubs();
		Task<List<Race>> GetAllUserRaces();
    }
}

