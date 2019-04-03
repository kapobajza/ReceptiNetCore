using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Recepti.Filters;
using Recepti.Helpers;
using Recepti.Repository.KorisnikRepo;
using Recepti.ViewModels.Account;

namespace Recepti.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKorisnikRepo _korisnikRepo;

        public AccountController(IKorisnikRepo korisnikRepo)
        {
            _korisnikRepo = korisnikRepo;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ModelStateValidate]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = _korisnikRepo.Get(x => x.KorisnickoIme == model.KorisnickoIme);

            if (user == null)
            {
                ModelState.AddModelError("LoginGreska", "Korisničko ime ili lozinka su netačni");
                return View();
            }

            if (user.Banovan)
            {
                ModelState.AddModelError("LoginGreska", "Žao name je, ali vi ste banovani. Ukoliko Vam je nejasno zašto, obratite se administratoru");
                return View();
            }

            var pw = HashingPasswords.GenerateHashArgon2(model.Password, user.PasswordSalt);

            if (user.PasswordHash != pw)
            {
                ModelState.AddModelError("LoginGreska", "Korisničko ime ili lozinka su netačni");
                return View();
            }

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.KorisnickoIme),
                new Claim(ClaimTypes.Role, user.Uloga),
                new Claim("Id", user.KorisnikId.ToString())
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            await Authentication.Login(HttpContext, principal);

            return Redirect(model.ReturnUrl ?? "/");
        }

        public async Task<IActionResult> Logout()
        {
            await Authentication.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [CustomExceptionHandler]
        [ModelStateValidate]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var salt = HashingPasswords.GenerateSalt();
            var pw = HashingPasswords.GenerateHashArgon2(model.Lozinka, salt);
            var uloga = nameof(TipKorisnikaEnum.Korisnik);

            _korisnikRepo.Add(new Models.Korisnik()
            {
                PasswordHash = pw,
                PasswordSalt = salt,
                Uloga = uloga,
                KorisnickoIme = model.KorisnickoIme,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Banovan = false
            });
            _korisnikRepo.SaveChanges();

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, model.KorisnickoIme),
                new Claim(ClaimTypes.Role, uloga)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            await Authentication.Login(HttpContext, principal);

            return RedirectToAction("Index", "Home");
        }
    }
}