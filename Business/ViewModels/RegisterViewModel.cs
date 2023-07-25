using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Email { get; set; }
    }
}
