
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyNetWebApp.Models
{
	public class AppUser : IdentityUser
	{
        //[Key]
        //public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Milage { get; set; }
		[ForeignKey("Address")]
		public int AddressId { get; set; }
		public Address? Address { get; set; }
		public ICollection<Club> Clubs { get; set; }
		public ICollection<Race> Races { get; set; }

	}
}

