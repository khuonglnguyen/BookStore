﻿using BookStore.Data.Repositories;
using BookStore.WebApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnURL = "/")
        {
            return View(new Login { returnURL = returnURL });
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult LoginWithGoogle(string returnURL = "/")
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback"),
                Items =
                {
                    {"returnURL", returnURL}
                }
            };

            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            var claims = result.Principal.Claims;

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimPricipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPricipal);

            return LocalRedirect(result.Properties.Items["returnURL"]);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = _userRepository.GetByUsernameAndPassword(login.Username, login.Password);
            if (user is null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FavoriteColor", user.FavoriteColor)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = login.RememberMe });
            return LocalRedirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
