using MyNetWebApp.Data.Enum;
using MyNetWebApp.Models;

namespace MyNetWebApp.ViewModels
{
	public class CreateClubViewModel
	{
		public CreateClubViewModel()
		{
		}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public string AppUserId { get; set; }
    }
}

