using System.ComponentModel.DataAnnotations;

namespace Shop.PL.Models.Identity
{
	public class SignUpViewModel
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;
		[Required(ErrorMessage = "UserName Is Requerd YA Hamada")]
		public string UserName { get; set; } = null!;
		[EmailAddress(ErrorMessage = "Email Is Requerd YA Hamada")]
		public string Email { get; set; } = null!;
		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?=(?:.*(.))(?!\1).{5,}).{6,}$",
		ErrorMessage = "Password must have at least 6 characters, including at least one digit, one lowercase letter, one uppercase letter, one non-alphanumeric character, and at least 2 unique characters.")]
		public string Password { get; set; } = null!;
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "ConfirmPassword Dosnt Match Password")]
		public string ConfirmPasword { get; set; } = null!;
		[Required]
		public bool IaAgreee { get; set; }
	}
}
