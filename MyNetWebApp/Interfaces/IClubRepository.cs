using MyNetWebApp.Models;

namespace MyNetWebApp.Interfaces
{
	public interface IClubRepository
	{
		Task<IEnumerable<Club>> GetAll();
		Task<IEnumerable<Club>> GetClubsByCity(string city);
		Task<Club> GetByIdAsync(int id);
		Task<Club> GetByIdAsyncNoTracking(int id);
		Task<IEnumerable<Club>?> GetClubByCity(string city);
		Task<IEnumerable<Club>?> GetClubsByTitle(string title);
        bool Add(Club club);
		bool Update(Club club);
		bool Delete(Club club);
		bool Save();
    }
}

