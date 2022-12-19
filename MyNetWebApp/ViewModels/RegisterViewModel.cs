using System;
using System.ComponentModel.DataAnnotations;

namespace MyNetWebApp.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Email address is required")]
		[Display(Name ="Email address")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [Display(Name = "Confirm password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}

