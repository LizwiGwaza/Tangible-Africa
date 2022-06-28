using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Tangible_Africa.Models.Data.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Tangible_Africa.Controllers
{
    public class AccountController : Controller
    {
        FirebaseAuthProvider auth;
        public AccountController()
        {
            auth = new FirebaseAuthProvider(
                         new FirebaseConfig("AlzaSyC2ZR3VQ13el2DgkMgsyftMCme_HT6dufM"));
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterVM register)
        {
            //create the user
            await auth.CreateUserWithEmailAndPasswordAsync(register.Email, register.Password);
            //log in the new user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(register.Email, register.Password);
            string token = fbAuthLink.FirebaseToken;
            //saving the token in a session variable
            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM  login)
        {
            //log in the user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(login.Email, login.Password);
            string token = fbAuthLink.FirebaseToken;
            //saving the token in a session variable
            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("SignIn");
        }
    }
}
