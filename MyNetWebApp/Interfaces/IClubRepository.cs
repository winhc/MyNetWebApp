﻿using MyNetWebApp.Models;

namespace MyNetWebApp.Interfaces
{
	public interface IClubRepository
	{
		Task<IEnumerable<Club>> GetAll();
		Task<IEnumerable<Club>> GetClubsByCity(string city);
		Task<Club> GetByIdAsync(int id);
		Task<Club> GetByIdAsyncNoTracking(int id);
        bool Add(Club club);
		bool Update(Club club);
		bool Delete(Club club);
		bool Save();
    }
}

