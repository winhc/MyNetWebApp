using System;
using MyNetWebApp.Models;

namespace MyNetWebApp.ViewModels
{
	public class UserViewModel
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public int? Pace { get; set; }
		public int? Mileage { get; set; }
		public string? ProfileImageUrl { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
		public Address? Address { get; set; }
    }
}

