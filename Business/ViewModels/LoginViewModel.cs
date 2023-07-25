using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
