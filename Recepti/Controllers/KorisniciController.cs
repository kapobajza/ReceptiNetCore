using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recepti.Filters;
using Recepti.Helpers;
using Recepti.Repository.KorisnikRepo;
using Recepti.ViewModels.Korisnici;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recepti.Controllers
{
    [Authorize]
    public class KorisniciController : Controller
    {
        private readonly IKorisnikRepo _korisnikRepo;

        public KorisniciController(IKorisnikRepo korisnikRepo)
        {
            _korisnikRepo = korisnikRepo;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var korisnici = _korisnikRepo.GetAll(x => x.Uloga != "Admin");
            var model = new List<KorisnikViewModel>();

            foreach (var item in korisnici)
            {
                model.Add(new KorisnikViewModel()
                {
                    Banovan = item.Banovan ? "Da" : "Ne",
                    ImePrezime = $"{item.Ime} {item.Prezime}",
                    KorisnickoIme = item.KorisnickoIme,
                    KorisnikId = item.KorisnikId,
                    BoolBanovan = item.Banovan
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Ban(int id)
        {
            var korisnik = _korisnikRepo.Get(x => x.KorisnikId == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            korisnik.Banovan = !korisnik.Banovan;
            _korisnikRepo.Update(korisnik);
            _korisnikRepo.SaveChanges();

            var banovanTekst = korisnik.Banovan ? "banovan" : "unbanovan";
            TempData["poruka"] = $"Korisnik uspješno {banovanTekst}.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Brisi(int id)
        {
            var korisnik = _korisnikRepo.Get(x => x.KorisnikId == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            _korisnikRepo.Remove(korisnik);
            _korisnikRepo.SaveChanges();

            TempData["poruka"] = "Korisnik uspješno izbrisan.";

            return RedirectToAction("Index");
        }

        [Route("/Profil")]
        public IActionResult Izmijeni()
        {
            int.TryParse(User?.FindFirst("Id")?.Value, out int korisnikId);

            var korisnik = _korisnikRepo.Get(x => x.KorisnikId == korisnikId);

            if (korisnik == null)
            {
                return NotFound();
            }

            var model = new IzmijeniKorisnikaViewModel()
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime
            };

            return View(model);
        }

        [Route("/Profil")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidate]
        public IActionResult Izmijeni(IzmijeniKorisnikaViewModel model)
        {
            int.TryParse(User?.FindFirst("Id")?.Value, out int korisnikId);

            var korisnik = _korisnikRepo.Get(x => x.KorisnikId == korisnikId);

            if (korisnik == null)
            {
                return NotFound();
            }

            korisnik.Ime = model.Ime;
            korisnik.Prezime = model.Prezime;

            _korisnikRepo.Update(korisnik);
            _korisnikRepo.SaveChanges();

            TempData["poruka"] = "Korisnički podaci uspješno izmijenjeni.";
            return RedirectToAction("Index", "Home");
        }

        [Route("/PromijeniLozinku")]
        public IActionResult PromijeniLozinku()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidate]
        [Route("/PromijeniLozinku")]
        public async Task<IActionResult> PromijeniLozinku(PromijeniLozinkuViewModel model)
        {
            int.TryParse(User?.FindFirst("Id")?.Value, out int korisnikId);

            var korisnik = _korisnikRepo.Get(x => x.KorisnikId == korisnikId);

            if (korisnik == null)
            {
                return NotFound();
            }

            var oldPw = HashingPasswords.GenerateHashArgon2(model.TrenutnaLozinka, korisnik.PasswordSalt);

            if (korisnik.PasswordHash != oldPw)
            {
                ModelState.AddModelError("TrenutnaLozinka", "Lozinka je netačna");
                return View(model);
            }

            var salt = HashingPasswords.GenerateSalt();
            var pw = HashingPasswords.GenerateHashArgon2(model.NovaLozinka, salt);

            korisnik.PasswordHash = pw;
            korisnik.PasswordSalt = salt;

            _korisnikRepo.Update(korisnik);
            _korisnikRepo.SaveChanges();

            await Authentication.Logout(HttpContext);
            return RedirectToAction("Login", "Account");
        }
    }
}