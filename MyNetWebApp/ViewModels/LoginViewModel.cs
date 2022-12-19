using System;
using System.ComponentModel.DataAnnotations;
namespace MyNetWebApp.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email address is required")]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
    }
}

