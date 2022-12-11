using System;
using MyNetWebApp.Models;

namespace MyNetWebApp.Interfaces
{
	public interface IRaceRepository
	{
        Task<IEnumerable<Race>> GetAll();
        Task<IEnumerable<Race>> GetRacesByCity(string city);
        Task<Race> GetByIdAsync(int id);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}

