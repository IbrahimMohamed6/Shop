using System.ComponentModel.DataAnnotations;

namespace Shop.PL.Models.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Requierd Ya Hamada")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;
    }
}
