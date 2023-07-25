using Business.UnitOfWork;
using Business.ViewModels;
using DataAccess.Base;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public AccountApiController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public object Login([FromForm] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ReturnViewModel { IsSuccess = false, Message = "Lütfen gerekli alanları doldurunuz." };
            }
            var loginCheck = _unitOfWork.Accounts.GetLoginCheck(loginViewModel.UserName, loginViewModel.Password);
            if (!loginCheck.IsSuccess || loginCheck.Data == null)
            {
                return new ReturnViewModel { IsSuccess = false, Message = "Kullanıcı adı veya şifre yanlış." };
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginViewModel.UserName),
                // Add more claims as needed (e.g., roles, permissions)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = loginViewModel.RememberMe // Set to true to create a persistent cookie
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return loginCheck;
        }


        [HttpPost("Logout")]
        public object Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new ReturnViewModel { IsSuccess = true, Message = "Başarıyla çıkış yapıldı" };
        }

        [HttpPost("Register")]
        public object Register([FromForm] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ReturnViewModel { IsSuccess = false, Message = "Lütfen gerekli alanları doldurunuz." };
            }
            var user = new User { Email = registerViewModel.Email, UserName = registerViewModel.UserName, Password = registerViewModel.Password };

            var registerResult = _unitOfWork.Accounts.Register(user);

            if (registerResult.IsSuccess)
            {
                _unitOfWork.SaveChanges();
            }
            else
            {
                _unitOfWork.RollBack();
            }
            return registerResult;

        }


    }
}
