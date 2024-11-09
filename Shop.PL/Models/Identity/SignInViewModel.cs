using System.ComponentModel.DataAnnotations;

namespace Shop.PL.Models.Identity
{
	public class SignInViewModel
	{
		[EmailAddress(ErrorMessage = "Email Is Requerd YA Hamada")]
		public string Email { get; set; } = null!;
		[DataType(DataType.Password, ErrorMessage = "Password Is Requerd YA Hamada")]
		public string Password { get; set; } = null!;

		public bool RemeberMe { get; set; }
	}
}
